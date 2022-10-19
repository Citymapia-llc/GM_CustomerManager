using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GM.Store.ImportV1
{
    public  class Import
    {
        public async Task<bool> ImportLargeFile(string excelFilePath, string mapperFilePath, Func<Dictionary<string, string>, string, bool, Task<int>> OnRecordProccessed = null)
        {

            try
            {
                int referanceId = 0;

                var fileName = excelFilePath.ToLower().Replace('\\', '/').Split("/".ToCharArray()).Last();

                var recordIndex = 0;
                var bulkInserIndex = 0;
                var degreeOfParallelism = 1;// Environment.ProcessorCount;

                //for (int i = 0; i < degreeOfParallelism; i++)
                //{
                //    //repos.Add(new RepositoryParts(true));
                //}

                new GM.Store.ExcelFileV1.Importer().ImportToObjectList(excelFilePath, mapperFilePath, async (rowDataList) =>
                {
                    Console.WriteLine($"Records ready to insert:{rowDataList.Count()}");
                    //var partsList = string.Join(", ", rowDataList.Select(a => a["Email"]).ToArray());
                    //OnRecordProccessed($"To be inserted:{partsList}", false);

                    var totalRecords = rowDataList.Count();
                    Parallel.For(0,
                    degreeOfParallelism, async workerId =>
                    {
                        //OnRecordProccessed($"Number of Processors:{degreeOfParallelism}", tr);

                        var max = totalRecords * (workerId + 1) / degreeOfParallelism;
                        //repos[workerId].BatchSize = totalRecords / degreeOfParallelism;
                        //repos[workerId].BulkInsertIndex = 0;
                        //OnRecordProccessed($"Max number of rcords:{max}", false);
                        for (int i = totalRecords * workerId / degreeOfParallelism; i < max; i++)
                        {
                            var rowData = rowDataList[i];

                            await OnRecordProccessed(rowData, "Success", true);
                            recordIndex++;
                        }
                    });
                    //Console.ReadKey();
                    return 1;
                }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("ImportLargeFile",ex.Message);
                OnRecordProccessed(null, ex.Message, false);
                return false;
            }
            return true;
        }
        #region

        //public async Task<bool> ImportLargeFile(string excelFilePath, string mapperFilePath, Func<Dictionary<string, string>, string, bool, Task<int>> OnRecordProccessed = null)
        //{
        //    int result = 0;
        //    try
        //    {
        //        int referanceId = 0;

        //        var fileName = excelFilePath.ToLower().Replace('\\', '/').Split("/".ToCharArray()).Last();

        //        var recordIndex = 0;
        //        var bulkInserIndex = 0;
        //        //var degreeOfParallelism = Environment.ProcessorCount;
        //        var degreeOfParallelism = 1;

        //        new GM.ExcelFile.Importer(excelFilePath, mapperFilePath, "").ParseExcelToList(excelFilePath, (rowDataList) =>
        //        {
        //            Console.WriteLine($"Records ready to insert:{rowDataList.Count()}");
        //            var totalRecords = rowDataList.Count();
        //            Parallel.For(0,
        //            degreeOfParallelism, workerId =>
        //            {
        //                 //OnRecordProccessed($"Number of Processors:{degreeOfParallelism}", tr);
        //                 var max = totalRecords * (workerId + 1) / degreeOfParallelism;
        //                 //repos[workerId].BatchSize = totalRecords / degreeOfParallelism;
        //                 //repos[workerId].BulkInsertIndex = 0;
        //                 //OnRecordProccessed($"Max number of rcords:{max}", false);
        //                 for (int i = totalRecords * workerId / degreeOfParallelism; i < max; i++)
        //                {
        //                    var rowData = rowDataList[i];

        //                    result = OnRecordProccessed(rowData, "Success", true).Result;
        //                    recordIndex++;
        //                }
        //            });
        //            //Console.ReadKey();
        //            return Task.FromResult(result);
        //        }
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        OnRecordProccessed(null, ex.Message, false);
        //        return false;
        //    }
        //    return result == 1 ? true : false;
        //}
        #endregion
    }
}
