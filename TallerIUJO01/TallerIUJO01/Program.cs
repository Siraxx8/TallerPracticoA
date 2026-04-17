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
			string registroUsuario = "		ID_777;		EVALUACION;		Jose Sira;		95" ;
			Console.WriteLine(registroUsuario);
			
			string registrolimpio = registroUsuario.Trim().ToUpper();
			Console.WriteLine(registrolimpio);
			
			string[] partes = registrolimpio.Split(';');
			string id = partes[0].Trim();
			string idnuevo = id.Replace('_',' ');
			string nombre = partes[1].Trim();
			string tarea = partes[2].Trim();
			string nota = partes[3].Trim();
			
			Console.WriteLine(string.Format("El ID llamado: {0} del usuario: {1} con la tarea: {2} tiene nota de: {3}", idnuevo ,nombre, tarea, nota));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}