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

// [START spanner_dml_getting_started_insert]

using Google.Cloud.Spanner.Data;
using System;
using System.Threading.Tasks;

public class WriteUsingDmlCoreAsyncSample
{
    public async Task<int> WriteUsingDmlCoreAsync(string projectId, string instanceId, string databaseId)
    {
        string connectionString = $"Data Source=projects/{projectId}/instances/{instanceId}/databases/{databaseId}";

        using var connection = new SpannerConnection(connectionString);
        await connection.OpenAsync();

        SpannerCommand cmd = connection.CreateDmlCommand(
            "INSERT Singers (SingerId, FirstName, LastName) VALUES "
               + "(12, 'Melissa', 'Garcia'), "
               + "(13, 'Russell', 'Morales'), "
               + "(14, 'Jacqueline', 'Long'), "
               + "(15, 'Dylan', 'Shaw')");
        int rowCount = await cmd.ExecuteNonQueryAsync();

        Console.WriteLine($"{rowCount} row(s) inserted...");
        return rowCount;
    }
}
// [END spanner_dml_getting_started_insert]
