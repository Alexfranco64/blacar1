using System.Data.SqlClient;

namespace blacarapi.Models
{
    public class Blamodel
    {
        //variables 
        string ConnectionString = "Server=tcp:serverblacar.database.windows.net,1433;Initial Catalog=blacar;Persist Security Info=False;User ID=lfrblacar;Password=642Cumple;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //Propiedades 
        public int ID { get; set; }
        public string? Origen { get; set; }
        public string? Destino { get; set; }
        public string? ImageBase64 { get; set; }
        public DateTime DateTime { get; set; }
        public int Nopasajeros { get; set; }
        public double Tarifa { get; set; }
        public string? Viajeredondo { get; set; }
        public PositionModel ActualPosition { get; set; }
        public List<PositionModel> RouterLastTravel { get; set; }
        //metodos
        public ApiResponse GetAll()
        {
            List<Blamodel> list = new List<Blamodel>();
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        string tsql = "SELECT * FROM Blacar " +
                                      "INNER JOIN Position ON Blacar.IDActualPosition = Position.IDPosition";
                        using (SqlCommand cmd = new SqlCommand(tsql, conn))
                        {
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Blamodel model = new Blamodel
                                    {
                                        ID = (int)reader["IDBlacar"],
                                        Origen = reader["Origen"].ToString(),
                                        Destino = reader["Destino"].ToString(),
                                        ImageBase64 = reader["ImageBase64"].ToString(),
                                        DateTime = DateTime.Now,
                                        Nopasajeros = (int)reader["Nopasajeros"],
                                        Tarifa = (double)reader["Tarifa"],
                                        Viajeredondo = reader["Viajeredondo"].ToString(),
                                        ActualPosition =  new PositionModel
                                        {
                                            Id = (int)reader["IDPosition"],
                                            Latitude = (double)reader["Latitude"],
                                            Longitude = (double)reader["Longitude"]
                                        }
                                    };
                                    ApiResponse routerResponse = new PositionModel().GetAllByBlacar(model.ID);
                                    if (routerResponse.IsSuccess) model.RouterLastTravel = routerResponse.Result as List<PositionModel>;

                                    list.Add(model);

                                }
                            }
                        }
                    }
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Message = "los viajes fueron obtenidos",
                        Result = list
                    };
                }
                catch (Exception ex) 
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = $"se genero un error:{ex.Message}",
                        Result = null
                    };
                }
            }
        }
    }
}
