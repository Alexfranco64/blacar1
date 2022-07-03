using System.Data.SqlClient;

namespace blacarapi.Models
{
    public class PositionModel
    {
        // Variables
        string ConnectionString = "Server=tcp:serverblacar.database.windows.net,1433;Initial Catalog=blacar;Persist Security Info=False;User ID=lfrblacar;Password=642Cumple;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ApiResponse GetAllByBlacar(int idBlacar)
        {
            List<PositionModel> list = new List<PositionModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT Position.* FROM Route " +
                                  "INNER JOIN PositionsXRoute ON Route.IDRoute = PositionsXRoute.IDRoute " +
                                  "INNER JOIN Position ON PositionsXRoute.IDPosition = Position.IDPosition " +
                                  "WHERE IDBlacar = @IDBlacar";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDBlacar", idBlacar);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PositionModel
                                {
                                    Id = (int)reader["IDPosition"],
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                });
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Las posiciones fueron obtenidos correctamente",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al obtener las posiciones: {ex.Message}",
                    Result = null
                };
            }
        }

        public ApiResponse GetLastByBlacar(int idBlacar)
        {
            List<PositionModel> list = new List<PositionModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT Position.* FROM Position " +
                                  "INNER JOIN PositionsXRoute ON Position.IDPosition = PositionsXRoute.IDPosition " +
                                  "WHERE IDRoute = (SELECT TOP 1 IDRoute FROM Route WHERE IDBlacar = @IDDriver ORDER BY Date DESC)";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDBlacar", idBlacar);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new PositionModel
                                {
                                    Id = (int)reader["IDPosition"],
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                });
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Las posiciones fueron obtenidos correctamente",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Se generó un error al obtener las posiciones: {ex.Message}",
                    Result = null
                };
            }
        }

    }

}
