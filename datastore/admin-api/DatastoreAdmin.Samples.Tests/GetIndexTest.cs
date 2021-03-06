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

using Grpc.Core;
using System.Linq;
using Xunit;

[Collection(nameof(DatastoreAdminFixture))]
public class GetIndexTest
{
    private readonly DatastoreAdminFixture _datastoreAdminFixture;

    public GetIndexTest(DatastoreAdminFixture datastoreAdminFixture)
    {
        _datastoreAdminFixture = datastoreAdminFixture;
    }

    [Fact]
    public void TestGetIndex()
    {
        // Currently, we don't have an API to create an index.
        // The test just verifies that if any index exists then GetIndex does not throw any Exception.
        ListIndexesSample listIndexesSample = new ListIndexesSample();
        GetIndexSample getIndexSample = new GetIndexSample();
        var indexes = listIndexesSample.ListIndexes(_datastoreAdminFixture.ProjectId).ToList();
        if (indexes.Any())
        {
            var firstIndexId = indexes.First().IndexId;
            var index = getIndexSample.GetIndex(_datastoreAdminFixture.ProjectId, firstIndexId);
            Assert.Equal(firstIndexId, index.IndexId);
        }
        else
        {
            var exception = Assert.Throws<RpcException>(() => getIndexSample.GetIndex(_datastoreAdminFixture.ProjectId, "random-index-id"));
            Assert.Equal(StatusCode.InvalidArgument, exception.StatusCode);
        }
    }
}
