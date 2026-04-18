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
			
			//1. el dato del usuario
			string registroUsuario = "		ID_777;		Jose Sira;		EVALUACION;		95;\n" ;
			Console.WriteLine(registroUsuario);
			
			//registro limpio del usuario
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
				Console.WriteLine("reporte estudiante realizado.\n");
			}
			
			//Persistencia binaria "fileStream"
			string archivoBin = Path.Combine(rutaraiz, "auditoria.dat");
			using (FileStream fs = new FileStream(archivoBin, FileMode.Append, FileAccess.Write)) {
				
				byte[] bytesID = Encoding.UTF8.GetBytes(id + "|");
				fs.Write(bytesID, 0, bytesID.Length);
				Console.WriteLine("Auditoría binaria creada.\n");
			}
			
			//Inspección de metadatos "fileInfo"
			FileInfo info = new FileInfo(archivotexto);
			Console.WriteLine("[ESTADÍSTICAS] El archivo de notas pesa: {0} bytes", info.Length);
			
			
			
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
