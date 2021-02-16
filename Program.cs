using System;
using Microsoft.AnalysisServices.Tabular;

namespace Learning_TOM {

  class Program {

    static void Main() {

      Listing1();
      //Listing2();
      //Listing3();
      //Listing4();
      //Listing5();
      //Listing6();
      //Listing7();

      //DatasetManager.ConnectToPowerBIAsUser();
      //Database database = DatasetManager.CreateDatabase("Wingtip Sales 1");
      //DatasetManager.CreateWingtipSalesModel(database);

      //string newDatabaseName = "My Cloned Dataset Copy";
      //DatasetManager.CopyDatabase(DatabaseName, newDatabaseName);
      //database = DatasetManager.CreateDatabase(newDatabaseName);

    }

    static void Listing1() {

      string workspaceConnection = "powerbi://api.powerbi.com/v1.0/myorg/YOU_WORKSPACE";
      string connectString = $"DataSource={workspaceConnection};";
      Server server = new Server();
      server.Connect(connectString);

      foreach (Database database in server.Databases) {
        Console.WriteLine(database.Name);
      }

    }

    static void Listing2() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      string targetDatabaseName = "Tom Demo";
      Database database = server.Databases.GetByName(targetDatabaseName);

      Console.WriteLine("Name: " + database.Name);
      Console.WriteLine("ID: " + database.ID);
      Console.WriteLine("ModelType: " + database.ModelType);
      Console.WriteLine("CompatibilityLevel: " + database.CompatibilityLevel);
      Console.WriteLine("LastUpdated: " + database.LastUpdate);
      Console.WriteLine("EstimatedSize: " + database.EstimatedSize);
      Console.WriteLine("CompatibilityMode: " + database.CompatibilityMode);
      Console.WriteLine("LastProcessed: " + database.LastProcessed);
      Console.WriteLine("LastSchemaUpdate: " + database.LastSchemaUpdate);

    }

    static void Listing3() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      string targetDatabaseName = "Tom Demo";
      Database database = server.Databases.GetByName(targetDatabaseName);
      Model datasetModel = database.Model;

      foreach (Table table in datasetModel.Tables) {
        Console.WriteLine(table.Name);
      }

    }

    static void Listing4() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      Database dataset = server.Databases.GetByName("Tom Demo");
      Table tableSales = dataset.Model.Tables.Find("Sales");

      foreach (Column column in tableSales.Columns) {
        Console.WriteLine("Coulumn: " + column.Name);
      }

      foreach (Measure measure in tableSales.Measures) {
        Console.WriteLine("Measure: " + measure.Name);
        Console.WriteLine(measure.Expression);
      }


    }

    static void Listing5() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      Model dataset = server.Databases.GetByName("Tom Demo").Model;
      Table tableSales = dataset.Tables.Find("Sales");

      Measure salesRevenue = new Measure();
      salesRevenue.Name = "Sales Revenue";
      salesRevenue.Expression = "SUM(Sales[SalesAmount])";
      salesRevenue.FormatString = "$#,##0.00";

      tableSales.Measures.Add(salesRevenue);

      dataset.SaveChanges();

    }

    static void Listing6() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      Model dataset = server.Databases.GetByName("Tom Demo").Model;
      Table tableSales = dataset.Tables.Find("Products");
      Column columnListPrice = tableSales.Columns.Find("List Price");
      columnListPrice.FormatString = "$#,##0.00";

      dataset.SaveChanges();

    }

    static void Listing7() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      Model dataset = server.Databases.GetByName("Tom Demo").Model;
      Table tableSales = dataset.Tables.Find("Sales");

      foreach (Column column in tableSales.Columns) {
        column.IsHidden = true;
      }

      dataset.SaveChanges();

    }

    static void Listing8() {

      string connectString = DatasetManager.GetUserConnectString();
      Server server = new Server();
      server.Connect(connectString);

      string targetDatabaseName = "Tom Demo Starter";
      Database database = server.Databases.GetByName(targetDatabaseName);
      Model datasetModel = database.Model;

      foreach (Table table in datasetModel.Tables) {
        foreach (Column column in table.Columns) {
          if (column.DataType == DataType.DateTime) {
            column.FormatString = "yyyy-MM-dd";
          }
        }
      }

      datasetModel.SaveChanges();
    }

  }
}
