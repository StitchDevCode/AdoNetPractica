// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;

string connectionString = "Server=DEVCUMPDELL; Database=Producto;Trusted_Connection=true";

Menu();

void Menu()
{
    int opcion = 0;

    Console.WriteLine("Menu");
    Console.WriteLine("1. Listado de Productos");
    Console.WriteLine("2. Agregar Producto");
    Console.WriteLine("3. Editar Productos");
    Console.WriteLine("4. Eliminar Producto");

    opcion = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Escoja una opción: ", opcion);

    if (opcion == 1)
    {
        Console.Clear();
        ListadoProducto();
        Console.ReadKey();
    }

    if (opcion == 2)
    {
        Console.Clear();
        AgregarProducto();
        Console.ReadKey();
    }

    if (opcion == 3)
    {
        Console.Clear();
        Editar();
        Console.ReadKey();
    }

    if (opcion == 4)
    {
        Console.Clear();
        Eliminar();
        Console.ReadKey();
    }

}

void ListadoProducto()
{
    Console.WriteLine("Listado de Productos");
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string sql = "Select * from dbo.Producto";
        connection.Open();

        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Encabezado de la tabla
                Console.WriteLine("+------------+----------------------+------------+------------+");
                Console.WriteLine("| IdProducto | Nombre               | Precio     | Stock      |");
                Console.WriteLine("+------------+----------------------+------------+------------+");

                while (reader.Read())
                {
                    // Filas de la tabla
                    Console.WriteLine($"| {reader["IdProducto"],-10} | {reader["Nombre"],-20} | {reader["Precio"],-10} | {reader["Stock"],-10} |");
                }

                // Pie de la tabla
                Console.WriteLine("+------------+----------------------+------------+------------+");
            }
        }
    }
}

void AgregarProducto()
{
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();

    Console.Write("Precio: ");
    decimal precio = Convert.ToDecimal(Console.ReadLine());

    Console.Write("Stock: ");
    int stock = Convert.ToInt32(Console.ReadLine());

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string insert = "Insert into dbo.Producto (Nombre, Precio, Stock) Values (@Nombre, @Precio, @Stock)";
        connection.Open();
        using(SqlCommand command = new SqlCommand(insert, connection))
        {
            command.Parameters.AddWithValue("@Nombre", nombre);
            command.Parameters.AddWithValue("@Precio", precio);
            command.Parameters.AddWithValue("@Stock", stock);

            int rowAffect = command.ExecuteNonQuery();

            if (rowAffect > 0)
            {
                Console.WriteLine("Se ha agregado correctamente");
            }
        }
    }
}

void Editar()
{
    ListadoProducto();

    Console.Write("Selecciona el ID de un Producto: ");
    int idProducto = Convert.ToInt32(Console.ReadLine());

    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();

    Console.Write("Precio: ");
    decimal precio = Convert.ToDecimal(Console.ReadLine());

    Console.Write("Stock: ");
    int stock = Convert.ToInt32(Console.ReadLine());
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string sqlUpdate = "UPDATE dbo.Producto SET Nombre=@Nombre, Precio=@Precio, Stock=@Stock WHERE IdProducto=@IdProducto";
        connection.Open();
        using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
        {
            command.Parameters.AddWithValue("@IdProducto", idProducto);
            command.Parameters.AddWithValue("@Nombre", nombre);
            command.Parameters.AddWithValue("@Precio", precio);
            command.Parameters.AddWithValue("@Stock", stock);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Se ha actualizado correctamente");
            }
        }

    }
}

void Eliminar()
{
    ListadoProducto();

    Console.Write("Selecciona el ID del Producto para Eliminar");
    int IdProducto = Convert.ToInt32(Console.ReadLine());

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string sqlDELETE = "delete from dbo.Producto where IdProducto=@IdProducto";

        using (SqlCommand command = new SqlCommand(sqlDELETE, connection))
        {
            command.Parameters.AddWithValue("@IdProducto", IdProducto);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.Write("Se ha eliminado el producto correctamente");
            }
        }
    }
}
