// Copyright 2020 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// [START spanner_dml_write_then_read]

using Google.Cloud.Spanner.Data;
using System;
using System.Threading.Tasks;

public class WriteAndReadUsingDmlCoreAsyncSample
{
    public async Task<int> WriteAndReadUsingDmlCoreAsync(string projectId, string instanceId, string databaseId)
    {
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";

        using var connection = new SpannerConnection(connectionString);
        await connection.OpenAsync();

        using var createDmlCmd = connection.CreateDmlCommand(@"INSERT Singers (SingerId, FirstName, LastName) VALUES (11, 'Timothy', 'Campbell')");
        int rowCount = await createDmlCmd.ExecuteNonQueryAsync();
        Console.WriteLine($"{rowCount} row(s) inserted...");

        // Read newly inserted record.
        using var createSelectCmd = connection.CreateSelectCommand(@"SELECT FirstName, LastName FROM Singers WHERE SingerId = 11");
        using var reader = await createSelectCmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            Console.WriteLine($"{reader.GetFieldValue<string>("FirstName")}  {reader.GetFieldValue<string>("LastName")}");
        }
        return rowCount;
    }
}
// [END spanner_dml_write_then_read]
