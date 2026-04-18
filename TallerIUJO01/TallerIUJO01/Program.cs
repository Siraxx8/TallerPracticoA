using System;
using System.IO;
using System.Text;


namespace TallerIUJO01
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("***Taller 01***");
			
			//el dato del usuario
			//registro desordenado
			string registroUsuario = "		ID_777;		Jose Sira;		EVALUACION;		95;\n" ;
			Console.WriteLine(registroUsuario);
			
			//registro limpio del usuario
			//registro ordenado
			string registrolimpio = registroUsuario.Trim().ToUpper();
			Console.WriteLine(registrolimpio);
			
			//arreglo
			string[] partes = registrolimpio.Split(';');
			string id = partes[0].Trim();
			string nombre = partes[1].Trim();
			string tarea = partes[2].Trim();
			string nota = partes[3].Trim();
			
			Console.WriteLine(string.Format("\nEl ID: {0} con el nombre de usuario {1}, tiene la tarea de {2} con nota de: {3}\n", id,nombre,tarea,nota));

			//flujo en archivos			
			
			string rutaraiz = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DATOSIUJO");
			string rutareporte = Path.Combine(rutaraiz, "Reportes");
			
			
			
			if (!Directory.Exists(rutareporte)) {
				Directory.CreateDirectory(rutareporte);
				Console.WriteLine("directorio creado correctamente");
				
			}
			string archivotexto = Path.Combine(rutareporte, "notas.txt");
			using (StreamWriter sw = new StreamWriter(archivotexto, true)){
				
				sw.WriteLine(string.Format("ID: {0} | Estudiante: {1} | Nota: {2} | Tarea: {3} | Fecha: {4}", id, nombre, nota, tarea, DateTime.Now));
				Console.WriteLine("-reporte estudiante realizado.\n");
			}
			
			//Persistencia binaria "fileStream"
			string archivoBin = Path.Combine(rutaraiz, "auditoria.dat");
			using (FileStream fs = new FileStream(archivoBin, FileMode.Append, FileAccess.Write)) {
				
				byte[] bytesID = Encoding.UTF8.GetBytes(id + "|");
				fs.Write(bytesID, 0, bytesID.Length);
				Console.WriteLine("-Auditoría binaria creada.\n");
			}
			
			//Inspección de metadatos "fileInfo"
			FileInfo info = new FileInfo(archivotexto);
			Console.WriteLine("[ESTADÍSTICAS] El archivo de notas pesa: {0} bytes", info.Length);
			
			//Lectura secuencial "StreamReader"
 			Console.WriteLine("\n----Contenido actual del Reporte:---");
 			using (StreamReader sr = new StreamReader(archivotexto)) {
 				string linea;
 			while ((linea = sr.ReadLine()) != null) {
 				Console.WriteLine(" LÍNEA: " + linea);
 				}
 			}

			
			/*---DESAFIOS---*/
			
			//DESAFIO 1
			
			// Recibe una cadena: usuario;clave
			string usuarioyclave = "Josesira;admin123"; 

			//Separamos el usuario de la clave usando Split 
			string[] separador = usuarioyclave.Split(';');
			string clave = separador[1];

			//Verificamos si contiene la secuencia "123" 
			string rutaseguridad = Path.Combine(rutareporte, "seguridad.txt");
			if (clave.Contains("123")) {
				
    		//Persistencia de aviso con StreamWriter 
   			using (StreamWriter sw = new StreamWriter(rutaseguridad, true)) {
        	sw.WriteLine("Clave Débil detectada");
    		}
	    		Console.WriteLine("\n-Aviso de seguridad generado.");
			}
			
			//DESAFIO 2
			
			// Se usa FileStream para manejar la imagen byte a byte
            string rutaOrigen = @"C:\Users\Sira\Pictures\pruebajpg\avatar.jpg";
            string rutaDestino = "respaldo.jpg";

          	if (File.Exists(rutaOrigen)) { 
                using (FileStream fsorigen = new FileStream(rutaOrigen, FileMode.Open, FileAccess.Read)) 
                using (FileStream fsdestino = new FileStream(rutaDestino, FileMode.Create, FileAccess.Write)) { 
                   
                   byte[] buffer = new byte[1024]; 
                    int bytesLeidos;
                    while ((bytesLeidos = fsorigen.Read(buffer, 0, buffer.Length)) > 0) { 
                     	fsdestino.Write(buffer, 0, bytesLeidos); 
                    }
                }
                Console.WriteLine("-Imagen clonada exitosamente via FileStream.");
            }
            
            //DESAFIO 3
            
            //Buscando archivos pesados
            Console.WriteLine("\n-- Escaneando archivos pesados en Reportes...");
            string[] listaArchivos = Directory.GetFiles(rutareporte); 
			if (listaArchivos == null)
				return;

            foreach (string item in listaArchivos) {
               		FileInfo infoarchivo = new FileInfo(item); 
                	// 5KB son 5120 bytes 
             		if (infoarchivo.Length > 5120) { 
                    Console.WriteLine("Eliminando {0} por exceso de peso ({1} bytes)", infoarchivo.Name, infoarchivo.Length);
                    infoarchivo.Delete(); 
                }
            }
			
			Console.Write("\nPRESIONA CUALQUIER TECLA PARA SALIR. . .");
			Console.ReadKey(true);
		}
	}
}
