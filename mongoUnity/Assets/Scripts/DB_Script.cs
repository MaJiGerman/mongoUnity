using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.IO;
using System.Drawing; //para Image

public class DB_Script 
{
	//private const string MONGO_URI="mongodb://user_pruebas:1234@cluster0-shard-00-00-6zyhr.azure.mongodb.net:27017,cluster0-shard-00-01-6zyhr.azure.mongodb.net:27017,cluster0-shard-00-02-6zyhr.azure.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority";
	private const string MONGO_URI="mongodb://user_pruebas:1234@cluster0-shard-00-00-zcbg7.mongodb.net:27017,cluster0-shard-00-01-zcbg7.mongodb.net:27017,cluster0-shard-00-02-zcbg7.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true&w=majority";
	//private const string MONGO_URI="mongodb://localhost:27017";
	private const string DB_NAME="db_cloud";
	private const string COLECTION_NAME="colection_cloud_2";

	private MongoClient client;
	private IMongoDatabase database;
	private IMongoCollection<Model> usuarios;

	public void Init()
	{
		client = new MongoClient(MONGO_URI);
		database = client.GetDatabase(DB_NAME);

		usuarios = database.GetCollection<Model>(COLECTION_NAME);
		//Debug.Log("Conectado a " + DB_NAME);
	}
	public void Shutdown()
	{
		client = null;
		database = null;
	}

	public bool InsertarUsuario(string inputNombre)
	{
		Model newUser = new Model();
		newUser.nombre = inputNombre;
		newUser.estado = "CREADO";
		newUser.color = "BLANCO";

		usuarios.InsertOne(newUser);
		//Debug.Log("Nuevo Usuario Creado");
		return true;
	}


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

		var result2 =   await usuarios.FindOneAndUpdateAsync(
						Builders<Model>.Filter.Eq("nombre", inputNombre),
						Builders<Model>.Update.Set("estado", newEstado)
											.Push("datos", u)
		);
		//this.ObtenerLongitudLista(inputNombre);
		//Debug.Log("Elemento Anadido");
	}
	
	public async void AniadirImagenUsuario(string inputNombre)
	{
		byte[] img = FileToByteArray("Assets/Images/Pokemon.png");
		byte[] img2 = File.ReadAllBytes("Assets/Images/Poppy.png");

		var result =    await usuarios.FindOneAndUpdateAsync(
						Builders<Model>.Filter.Eq("nombre", inputNombre),
						Builders<Model>.Update.Set("imagen", img).Set("imagen2", img2)
		);
	}

	public byte[] FileToByteArray(string fileName)
	{
    byte[] fileData = null;

    using (FileStream fs = File.OpenRead(fileName)) 
    { 
        using (BinaryReader binaryReader = new BinaryReader(fs))
        {
            fileData = binaryReader.ReadBytes((int)fs.Length); 
        }
    }
    return fileData;
	}	

	public async void ActualizarColorUsuario(string inputNombre, string newColor)
	{
		var result2 =   await usuarios.FindOneAndUpdateAsync(
						Builders<Model>.Filter.Eq("nombre", inputNombre),
						Builders<Model>.Update.Set("color", newColor)
		);
		//Debug.Log("Color actualizado a "+newColor);
	}
	
	public async void AniadirListaUsuario(string inputNombre, string newEstado)
	{
		UserData u = new UserData();

		int timeHrs =  (int)System.DateTime.Now.Hour;
		int timeMin =  (int)System.DateTime.Now.Minute;
		int timeSec =  (int)System.DateTime.Now.Second;
        int timeMSec = (int)System.DateTime.Now.Millisecond;

		var result2 =   await usuarios.FindOneAndUpdateAsync(
						Builders<Model>.Filter.Eq("nombre", inputNombre),
						Builders<Model>.Update.Set("estado", newEstado)
											.Push("datos", u)
		);
		int endtimeHrs =  (int)System.DateTime.Now.Hour;
		int endtimeMin =  (int)System.DateTime.Now.Minute;
		int endtimeSec =  (int)System.DateTime.Now.Second;
        int endtimeMSec = (int)System.DateTime.Now.Millisecond;
		
		int opTotalTime = (endtimeHrs-timeHrs)*60*60*1000+(endtimeMin-timeMin)*60*1000+(endtimeSec-timeSec)*1000+(endtimeMSec-timeMSec);

        Debug.Log("ANIADIR " +inputNombre+ ". TIEMPO: " + opTotalTime + " ms. TAMAÑO "+ result2.getSize() +" bytes");
		//Debug.Log("Elemento Anadido");
	}
	
	public async void BorrarListaUsuario(string inputNombre, string newEstado)
	{
		int timeHrs =  (int)System.DateTime.Now.Hour;
		int timeMin =  (int)System.DateTime.Now.Minute;
		int timeSec =  (int)System.DateTime.Now.Second;
        int timeMSec = (int)System.DateTime.Now.Millisecond;

		var result2 = await usuarios.FindOneAndUpdateAsync(
                      Builders<Model>.Filter.Eq("nombre", inputNombre),
                      Builders<Model>.Update.Set("estado", newEstado)
											.PopLast("datos")
                      );

		int endtimeHrs =  (int)System.DateTime.Now.Hour;
		int endtimeMin =  (int)System.DateTime.Now.Minute;
		int endtimeSec =  (int)System.DateTime.Now.Second;
        int endtimeMSec = (int)System.DateTime.Now.Millisecond;

		int opTotalTime = (endtimeHrs-timeHrs)*60*60*1000+(endtimeMin-timeMin)*60*1000+(endtimeSec-timeSec)*1000+(endtimeMSec-timeMSec);

        Debug.Log("BORRAR " +inputNombre+ ". TIEMPO: " + opTotalTime + " ms. TAMAÑO "+ result2.getSize() + " bytes");
		//Debug.Log("Elemento Eliminado");
	}

	public async void BorrarUsuario(string inputNombre)
	{
		int timeHrs =  (int)System.DateTime.Now.Hour;
		int timeMin =  (int)System.DateTime.Now.Minute;
		int timeSec =  (int)System.DateTime.Now.Second;
        int timeMSec = (int)System.DateTime.Now.Millisecond;

		var result =     await usuarios.FindOneAndDeleteAsync(
                         Builders<Model>.Filter.Eq("nombre", inputNombre),
                         new FindOneAndDeleteOptions<Model>
                         {
                         	Sort = Builders<Model>.Sort.Descending("nombre")
                         }
                         );

		int endtimeHrs =  (int)System.DateTime.Now.Hour;
		int endtimeMin =  (int)System.DateTime.Now.Minute;
		int endtimeSec =  (int)System.DateTime.Now.Second;
        int endtimeMSec = (int)System.DateTime.Now.Millisecond;

		int opTotalTime = (endtimeHrs-timeHrs)*60*60*1000+(endtimeMin-timeMin)*60*1000+(endtimeSec-timeSec)*1000+(endtimeMSec-timeMSec);

        Debug.Log("ELIMINAR USUARIO" +inputNombre+ ". TIEMPO: " + opTotalTime + " ms");
		//Debug.Log("Usuario Eliminado");
	}

	public int ObtenerLongitudLista(string inputNombre)
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
			//Debug.Log("Datos: " + tamano);
		}

		return tamano;
	}

}
