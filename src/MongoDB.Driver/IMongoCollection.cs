﻿/* Copyright 2010-2014 MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver
{
    /// <summary>
    /// Represents a collection in MongoDB.
    /// </summary>
    /// <typeparam name="TDocument">The type of the documents stored in the collection.</typeparam>
    public interface IMongoCollection<TDocument>
    {
        /// <summary>
        /// Gets the namespace of the collection.
        /// </summary>
        CollectionNamespace CollectionNamespace { get; }

        /// <summary>
        /// Gets the document serializer.
        /// </summary>
        IBsonSerializer<TDocument> DocumentSerializer { get; }

        /// <summary>
        /// Gets the index manager.
        /// </summary>
        IMongoIndexManager<TDocument> IndexManager { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        MongoCollectionSettings Settings { get; }

        /// <summary>
        /// Runs an aggregation pipeline.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is a cursor.</returns>
        Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IEnumerable<object> pipeline, AggregateOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs multiple write operations.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of writing.</returns>
        Task<BulkWriteResult<TDocument>> BulkWriteAsync(IEnumerable<WriteModel<TDocument>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Counts the number of documents in the collection.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The number of documents in the collection.
        /// </returns>
        Task<long> CountAsync(object filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes multiple documents.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the delete operation.
        /// </returns>
        Task<DeleteResult> DeleteManyAsync(object filter, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes a single document.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the delete operation.
        /// </returns>
        Task<DeleteResult> DeleteOneAsync(object filter, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the distinct values for a specified field.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is a cursor.</returns>
        Task<IAsyncCursor<TResult>> DistinctAsync<TResult>(string fieldName, object filter, DistinctOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds the documents matching the filter.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is a cursor.</returns>
        Task<IAsyncCursor<TResult>> FindAsync<TResult>(object filter, FindOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds a single document and deletes it atomically.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The returned document.
        /// </returns>
        Task<TResult> FindOneAndDeleteAsync<TResult>(object filter, FindOneAndDeleteOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds a single document and replaces it atomically.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The returned document.
        /// </returns>
        Task<TResult> FindOneAndReplaceAsync<TResult>(object filter, TDocument replacement, FindOneAndReplaceOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds a single document and updates it atomically.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="update">The update.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The returned document.
        /// </returns>
        Task<TResult> FindOneAndUpdateAsync<TResult>(object filter, object update, FindOneAndUpdateOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a single document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the insert operation.
        /// </returns>
        Task InsertOneAsync(TDocument document, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts many documents.
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the insert operation.
        /// </returns>
        Task InsertManyAsync(IEnumerable<TDocument> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes a map-reduce command.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="map">The map function.</param>
        /// <param name="reduce">The reduce function.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task whose result is a cursor.</returns>
        Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TResult> options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Replaces a single document.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="replacement">The replacement.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the replacement.
        /// </returns>
        Task<ReplaceOneResult> ReplaceOneAsync(object filter, TDocument replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates many documents.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="update">The update.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        Task<UpdateResult> UpdateManyAsync(object filter, object update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates a single document.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="update">The update.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// The result of the update operation.
        /// </returns>
        Task<UpdateResult> UpdateOneAsync(object filter, object update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns a new collection with a different read preference.
        /// </summary>
        /// <param name="readPreference">The read preference.</param>
        /// <returns>A new collection.</returns>
        IMongoCollection<TDocument> WithReadPreference(ReadPreference readPreference);

        /// <summary>
        /// Returns a new collection with a different write concern.
        /// </summary>
        /// <param name="writeConcern">The write concern.</param>
        /// <returns>A new collection.</returns>
        IMongoCollection<TDocument> WithWriteConcern(WriteConcern writeConcern);
    }
}
