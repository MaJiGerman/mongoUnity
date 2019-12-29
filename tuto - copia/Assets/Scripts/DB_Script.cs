using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
public class DB_Script 
{
	//private const string MONGO_URI="mongodb+srv://user_pruebas:1234@cluster0-zcbg7.mongodb.net/test?w=majority";
	//private const string MONGO_URI="mongodb://user_pruebas:1234@cluster0-shard-00-00-zcbg7.mongodb.net:27017,cluster0-shard-00-01-zcbg7.mongodb.net:27017,cluster0-shard-00-02-zcbg7.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0";
	private const string MONGO_URI="mongodb://user_pruebas:1234@cluster0-shard-00-00-zcbg7.mongodb.net:27017,cluster0-shard-00-01-zcbg7.mongodb.net:27017,cluster0-shard-00-02-zcbg7.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority";
	private const string DB_NAME="prueba_cloud";
	private const string COLECTION_NAME="colection_cloud";

	private MongoClient client;
	private IMongoDatabase database;
	private IMongoCollection<Model> usuarios;

	public void Init()
	{
		client = new MongoClient(MONGO_URI);
		database = client.GetDatabase(DB_NAME);

		usuarios = database.GetCollection<Model>(COLECTION_NAME);
		Debug.Log("Conectado a " + DB_NAME);
	}
	public void Shutdown()
	{
		client = null;
		database = null;
	}

	#region Insert
	public bool InsertarUsuario(string usuario)
	{
		Model newUser = new Model();
		newUser.nombre = usuario;
		newUser.estado = "CREADO";
		newUser.color = "BLANCO";

		usuarios.InsertOne(newUser);
		Debug.Log("Nuevo Usuario Creado");
		return true;
	}
	#endregion

	#region Fetch
	public List<Model> BuscarUsuario(string inputNombre)
	{
		List<Model> retunArray = new List<Model>();

		var query =
    	usuarios.AsQueryable<Model>()
    	.Where(e => e.nombre == inputNombre);

		int count = 0;
		foreach(Model m in query)
		{
			retunArray.Add(m);

			count++;
			Debug.Log("ELEMENTO " + count);
			Debug.Log(m.ToString());
		}

		return retunArray;
	}

	public async void ActualizarUsuario(string inputNombre, string newEstado)
	{
		UserData u = new UserData();

		var result2 = await usuarios.FindOneAndUpdateAsync(
						Builders<Model>.Filter.Eq("nombre", inputNombre),
						Builders<Model>.Update.Set("estado", newEstado)
											.Push("datos", u)
		);
		this.GetDataLenght(inputNombre);
		Debug.Log("Elemento Anadido");
	}
	
	public async void BorrarListaUsuario(string inputNombre, string newEstado)
	{
		var result2 = await usuarios.FindOneAndUpdateAsync(
                      Builders<Model>.Filter.Eq("nombre", inputNombre),
                      Builders<Model>.Update.Set("estado", newEstado)
											.PopLast("datos")
                      );	
		Debug.Log("Elemento Eliminado");
	}

	public async void DeleteUsuario(string inputNombre)
	{
		var result = await usuarios.FindOneAndDeleteAsync(
                         Builders<Model>.Filter.Eq("nombre", inputNombre),
                         new FindOneAndDeleteOptions<Model>
                         {
                         	Sort = Builders<Model>.Sort.Descending("nombre")
                         }
                         );
		Debug.Log("Usuario Eliminado");
	}

	public int GetDataLenght(string inputNombre)
	{
		
		List<UserData> aux;
		int tamano = 0;
		var query =
    	usuarios.AsQueryable<Model>()
    	.Where(e => e.nombre == inputNombre);

		foreach(Model m in query)
		{
			aux=m.datos;
			tamano = aux.Count;
			Debug.Log("Datos: " + tamano);
		}

		return tamano;
	}
	#endregion
}
