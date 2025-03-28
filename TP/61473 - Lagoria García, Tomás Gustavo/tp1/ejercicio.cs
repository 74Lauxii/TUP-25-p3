using System;       // Para usar la consola  (Console)
using System.IO;    // Para leer archivos    (File)

// Ayuda: 
//   Console.Clear() : Borra la pantalla
//   Console.Write(texto) : Escribe texto sin salto de línea
//   Console.WriteLine(texto) : Escribe texto con salto de línea
//   Console.ReadLine() : Lee una línea de texto
//   Console.ReadKey() : Lee una tecla presionada

// File.ReadLines(origen) : Lee todas las líneas de un archivo y devuelve una lista de strings
// File.WriteLines(destino, lineas) : Escribe una lista de líneas en un archivo

// Escribir la solucion al TP1 en este archivo. (Borre el ejemplo de abajo)
struct Contacto{
    public int ID;
    public string Nombre;
    public string Telefono;
    public string Email;

    public Contacto(int id, string nombre, string telefono, string email){
        ID = id;
        Nombre = nombre;
        Telefono = telefono;
        Email = email;
    }
}
struct Agenda{
    static public int CantidadTotal=10;
    public Contacto[] Contactos;

    public Agenda(){
        Contactos = new Contacto[CantidadTotal];
        int CantidadContactos = 0;
    }
    public void AgregarContacto(){
        if(CantidadContactos < CantidadTotal){
            Console.WriteLine("=== Agregar Contacto ===");
            Console.WriteLine("Ingrese el nombre del contacto: ");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el telefono del contacto: ");
            string telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el email del contacto: ");
            string email = Console.ReadLine();
            int id=CantidadContactos+1;
            Contactos[CantidadContactos] = new Contacto(id, nombre, telefono, email);
            CantidadContactos++;
        }
         else if(CantidadContactos == CantidadTotal-1){
                Console.WriteLine("La agenda le queda un lugar disponible de memoria");
            }
          Console.WriteLine("La agenda está llena");
    }
    public void MostrarContactos(){
        Console.WriteLine("ID  Nombre  Telefono  Email ");
        for(int i = 0; i < CantidadContactos; i++){
            Console.WriteLine($"{Contactos[i].ID}  {Contactos[i].Nombre}  { Contactos[i].Telefono}  { Contactos[i].Email}");
            Console.WriteLine();
        }
    }
    public void BuscarContacto(){
        Console.WriteLine("Ingrese un termino de busqueda(telefono, nombre, id, ,Email) de contacto a buscar: ");
        string termino = Console.ReadLine();
        Console.WriteLine("=== Resultados de la búsqueda ===");
         Console.WriteLine("ID  Nombre  Telefono  Email ");
        for(int i = 0; i < CantidadContactos; i++){
            if(int.TryParse(termino, out int id) && Contactos[i].ID == id){
               Console.WriteLine($"{Contactos[i].ID}  {Contactos[i].Nombre}  { Contactos[i].Telefono}  { Contactos[i].Email}");
               Console.WriteLine();
            break;
            }
            if(Contactos[i].Nombre.ToLower().Contains(termino.ToLower()) || Contactos[i].Telefono.Contains(termino) || Contactos[i].Email.ToLower().Contains(termino.ToLower())){
                Console.WriteLine($"{Contactos[i].ID}  {Contactos[i].Nombre}  { Contactos[i].Telefono}  { Contactos[i].Email}");
                Console.WriteLine();    
            }
        }
        Console.WriteLine("No se encontró el contacto");
    }

    public void GuardarContactos(string archivo){
        string[] lineas = new string[CantidadContactos];
        for(int i = 0; i < CantidadContactos; i++){
            lineas[i] = Contactos[i].ID + ";" + Contactos[i].Nombre + ";" + Contactos[i].Telefono + ";" + Contactos[i].Email;
        }
        File.WriteAllLines(archivo, lineas);
    }
    public void CargarContactos(string archivo){
        string[] lineas = File.ReadAllLines(archivo);
        for(int i = 0; i < lineas.Length; i++){
            string[] campos = lineas[i].Split(';');
            Contacto contacto = new Contacto(campos[0], campos[1], campos[2], campos[3]);
            AgregarContacto(contacto);
        }
    }
    public void ModificarContacto(){
    Console.WriteLine("Ingrese el ID del contacto a modificar: ");
    int id = int.Parse(Console.ReadLine());
    for(int i = 0; i < CantidadContactos; i++){
        if(Contactos[i].ID == id){
            Console.WriteLine("Ingrese el nuevo nombre del contacto: ");
            Contactos[i].Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo telefono del contacto: ");
            Contactos[i].Telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo email del contacto: ");
            Contactos[i].Email = Console.ReadLine();
            Console.WriteLine("Contacto modificado");
            return;
        }
    }
    Console.WriteLine("No se encontró el contacto");   
    }
    public void BorrarContacto(){
    Console.WriteLine("Ingrese el ID del contacto a borrar: ");
    int id = int.Parse(Console.ReadLine());
    for(int i = 0; i < CantidadContactos; i++){
        if(Contactos[i].ID == id){
            for(int j = i; j < CantidadContactos - 1; j++){
                Contactos[j] = Contactos[j + 1];
            }
            CantidadContactos--;
            Console.WriteLine("Contacto eliminado");
            return;
        }
    }
    Console.WriteLine("No se encontró el contacto");
    }
}
Agenda agenda = new Agenda();
string archivo = "contactos.txt";
if(File.Exists(archivo)){
    agenda.CargarContactos(archivo);
}

Console.WriteLine("Hola, soy el ejercicio 1 del TP1 de la materia Programación 3");
Console.Write("Presionar una tecla para continuar...");
Console.ReadKey();
Console.Clear();
while(true){
     Console.Clear();
    Console.WriteLine("=== Agenda de Contactos ===");
    Console.WriteLine("1. Agregar contacto");
    Console.WriteLine("2. Mostrar contactos");
    Console.WriteLine("3. Buscar contacto");
    Console.WriteLine("4. Modificar contacto");
    Console.WriteLine("5. Borrar contacto");
    Console.WriteLine("6. Salir");
    Console.WriteLine("Ingrese una opción: ");
    string opcion = Console.ReadLine();
    switch(opcion){
        case "1":
            agenda.AgregarContacto();
            while(true){
                Console.WriteLine("Desea agregar otro contacto? (s/n)");
                string respuesta = Console.ReadLine();
                if(respuesta == "n"){
                    break;
                }
                else if(respuesta != "s"){
                    Console.WriteLine("Respuesta inválida");
                }
                else{
                    agenda.AgregarContacto();
                }
            }        
            break;
        case "2":
            agenda.MostrarContactos();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        case "3":
            agenda.BuscarContacto();
            Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        case "4":
            agenda.ModificarContacto();
            Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        case "5":
            agenda.BorrarContacto();
            Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        case "6":
          console.WriteLine("Saliendo de la agenda");
            agenda.GuardarContactos(archivo);
            return;
        default:
            Console.WriteLine("Opción inválida");
            break;
    }
}