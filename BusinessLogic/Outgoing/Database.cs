using Npgsql;

namespace Backend.Outgoing
{
    public class Database
    {
        private NpgsqlConnection con;

        public Database()
        {
            con = new NpgsqlConnection("Server=balarama.db.elephantsql.com;Port=5432;Database=qssnesmy;User Id=qssnesmy;Password=x9GDUYtzn910kSwR9rRQy-SnywVxiJK9;");
            con.Open();
        }

        private async Task EnsureTables()
        {
            await using (var cmd = new NpgsqlCommand($"CREATE TABLE IF NOT EXISTS users (uname VARCHAR UNIQUE NOT NULL, pword VARCHAR NOT NULL, joke VARCHAR, token VARCHAR UNIQUE);", con)) { await cmd.ExecuteNonQueryAsync(); }
            await using (var cmd = new NpgsqlCommand($"CREATE TABLE IF NOT EXISTS jokes (text VARCHAR UNIQUE NOT NULL, upvote INTEGER, downvote INTEGER);", con)) { await cmd.ExecuteNonQueryAsync(); }
        }

        public async Task InsertJoke(string text)
        {
            await EnsureTables();
            var sql = $"INSERT INTO jokes (text, upvote, downvote) VALUES ($1, 2, 0);";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue(text);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<bool> HasJoke(string text)
        {
            await EnsureTables();
            var sql = $"SELECT 1 FROM jokes WHERE text = $1;";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue(text);
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.HasRows;
                }
            }
        }

        public async Task BindJokeToUser(string token, string joke)
        {
            await EnsureTables();
            var sql = $"UPDATE users SET joke = $1 WHERE token = '{token}';";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue(joke);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<string?> Login(string uname, string pword)
        {
            await EnsureTables();
            var sql = $"UPDATE users SET token = gen_random_uuid () WHERE uname = '{uname}' AND pword = '{pword}' RETURNING token;";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                var reader = cmd.ExecuteScalar();
                return reader?.ToString();
            }
        }

        public async Task<string?> CreateUser(string uname, string pword)
        {
            await EnsureTables();
            var sql = $"INSERT INTO users (uname, pword, token) VALUES ('{uname}', '{pword}', gen_random_uuid ()) RETURNING token;";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        public async Task<string> GetGoodJoke()
        {
            await EnsureTables();
            var sql = $"SELECT text FROM jokes ORDER BY upvote / (upvote + downvote) + 0.15 * RANDOM() LIMIT 1;";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.GetString(0);
                }
            }
        }

        public async Task Upvote(string token)
        {
            await EnsureTables();
            var sql = $"UPDATE jokes SET upvote = upvote + 1 FROM users WHERE jokes.text = users.joke AND users.token = '{token}';";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Downvote(string token)
        {
            await EnsureTables();
            var sql = $"UPDATE jokes SET downvote = downvote + 1 FROM users WHERE jokes.text = users.joke AND users.token = '{token}';";
            await using (var cmd = new NpgsqlCommand(sql, con))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
}