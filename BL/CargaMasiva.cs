using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Text;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BL
{
    public class CargaMasiva
    {
        public static bool CargaMasivaBulkCopy(IFormFile file)
        {
            bool success = false;

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    StreamReader archivoLeido = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding(28591));

                    TextFieldParser parser = new TextFieldParser(new StringReader(archivoLeido.ReadToEnd()));

                    // You can also read from a file
                    // TextFieldParser parser = new TextFieldParser("mycsvfile.csv");
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");

                    string[] fields;
                    bool bandera = false;

                    while (!parser.EndOfData)
                    {
                        fields = parser.ReadFields();

                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (bandera == false)
                            {
                                /*
                                string palabaConTildes = "áéíóúñ";
                                string palabaSinTildes = Regex.Replace(palabaConTildes.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
                                */
                                //dt.Columns.Add(Regex.Replace(fields[i].Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", ""));;
                                /*byte[] tempBytes;
                                tempBytes = System.Text.Encoding.GetEncoding(28591).GetBytes(fields[i]);
                                string sinAcento = System.Text.Encoding.UTF8.GetString(tempBytes);*/

                                dt.Columns.Add(fields[i]);
                            }
                            else
                            {
                                int count = 0;

                                DataRow row = dt.NewRow();


                                if (fields[0] == "" || fields[0] == null)
                                {
                                    fields[0] = "-";
                                    count++;
                                }
                                row["Nombre"] = fields[0];

                                if (fields[1] == "" || fields[1] == null)
                                {
                                    fields[1] = "0";
                                    count++;
                                }
                                row["Precio Unitario"] = decimal.Parse(fields[1].Replace("$", string.Empty).Trim());

                                if (fields[2] == "" || fields[2] == null)
                                {
                                    fields[2] = "0";
                                    count++;
                                }
                                row["Stock"] = int.Parse(fields[2]);

                                if (fields[3] == "" || fields[3] == null)
                                {
                                    fields[3] = "-";
                                    count++;
                                }
                                row["Descripción"] = fields[3];


                                if (fields[4] == "" || fields[4] == null)
                                {
                                    fields[4] = "-";
                                    count++;
                                }
                                else
                                {
                                    //FECHA
                                    string[] fecha = fields[4].Split(' ');
                                    string fechaNumero = fecha[1].Trim();
                                    DateTime fechaConvertida = DateTime.ParseExact(fechaNumero, "dd-MM-yy", null);
                                    row["FechaRegistro"] = fechaConvertida.ToString("yy-MM-dd");
                                }

                                row["Separador"] = fields[5];

                                if (fields[6] == "" || fields[6] == null)
                                {
                                    fields[6] = "0";
                                    count++;
                                }
                                row["IdProveedor"] = int.Parse(fields[6]);

                                if (fields[7] == "" || fields[7] == null)
                                {
                                    fields[7] = "-";
                                    count++;
                                }
                                row["Proveedor"] = fields[7];

                                if (fields[8] == "" || fields[8] == null)
                                {
                                    fields[8] = "0";
                                    count++;
                                }
                                Int64 number = Int64.Parse(fields[8].Trim(), System.Globalization.NumberStyles.Float);
                                fields[8] = number.ToString();
                                row["Numero"] = Int64.Parse(fields[8]);

                                if (fields[9] == "" || fields[9] == null)
                                {
                                    fields[9] = "-";
                                    count++;
                                }
                                row["Direccion"] = fields[9];

                                if (count != 9)
                                {
                                    dt.Rows.Add(row);
                                }
                                break;
                            }
                        }

                        /*
                            if  (bandera == false)
                            {
                                foreach (var registro in fields)
                                {
                                    dt.Columns.Add(registro);
                                }
                                bandera = true;
                            }
                            else
                            { 
                                foreach (var registro in fields)
                                {
                                    DataRow row = dt.NewRow();
                                    dt.Rows.Add(registro);
                                }
                            }*/
                        bandera = true;
                    }
                    parser.Close();

                    using (SqlTransaction transaction = context.BeginTransaction())
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(context, SqlBulkCopyOptions.Default, transaction))
                        {
                            try
                            {
                                bulkCopy.DestinationTableName = "Producto";
                                //bulkCopy.WriteToServer();
                                transaction.Commit();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                context.Close();
                                throw;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var excepcion = ex;
                success = false;
            }
            return success;
        }
    }
}