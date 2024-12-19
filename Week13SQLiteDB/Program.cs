
using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{

    SQLiteConnection connection = new SQLiteConnection("Data Source = mydb.db; version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found. Connection opened.");

    }
    catch
    {
        Console.WriteLine("DB not found. Created a new blank file.");
    }

    return connection;

}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;
    
    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * from customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"ID: {readerRowId} Name: {readerStringFirstName} {readerStringLastName}; DoB '{readerStringDoB}'");
    }
    myConnection.Close();
}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, dob;
    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth(mm-dd-yyy):");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES('{fName}', '{lName}', '{dob}')";

    int rowsAdded = command.ExecuteNonQuery();
    Console.WriteLine($"Amount of rows inserted: {rowsAdded}");


    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToRemove;
    Console.WriteLine("Enter customer ID to remove:");
    idToRemove = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid ={idToRemove}";
    int rowsRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowsRemoved} rows were removed from table 'customer'");
}

