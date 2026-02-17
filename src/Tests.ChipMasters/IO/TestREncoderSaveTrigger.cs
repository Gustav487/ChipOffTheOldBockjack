using ChipMasters.IO;
using ChipMasters.User;
using Fakes.ChipMasters.Users;
using System.Text.Json;

namespace Tests.ChipMasters.IO
{
    public class TestREncoderSaveTrigger
    {
        [TestClass]
        public class Valid
        {
            private string _tempDir = null!;
            private string _mainPath = null!;
            private string _backupPath = null!;

            private const string json = @"
{
    ""chips"": 0,
    ""metrics"": {
        ""match_history"": [],
        ""chip_history"": [],
        ""stats"": {}
    },
    ""inventory"": {
        ""items"": []
    },
    ""assets"": {
        ""card_flip"": ""one_eighty"",
        ""card_skin"": ""_"",
        ""game_bg"": ""_"",
        ""gui"": ""_""
    },
    ""achievements"": [],
    ""settings"": {
        ""volume_levels"": {},
        ""brightness_level"": 1
    },
    ""avatar"": {
        ""parts"": {
            ""background"": 0,
            ""skin"": 0,
            ""hair"": 0,
            ""eyes"": 0,
            ""mouth"": 0
        },
        ""name"": """"
    }
}";

            [TestInitialize]
            public void Setup()
            {
                _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(_tempDir);
                _mainPath = Path.Combine(_tempDir, "user.json");
                _backupPath = _mainPath + ".backup";
            } // end Setup()

            [TestCleanup]
            public void Teardown() => Directory.Delete(_tempDir, recursive: true);

            [TestMethod]
            public async Task SaveAndClose()
            {
                var saveTrigger = new REncoderSaveTrigger<IUser>(_mainPath, IUser.P1_ENDEC, new FakeUser());

                saveTrigger.Save();
                await saveTrigger.Close();

                Assert.IsTrue(File.Exists(_mainPath));
                Assert.IsTrue(File.Exists(_backupPath));

                string content = File.ReadAllText(_mainPath);
                var expectedJson = JsonSerializer.Deserialize<JsonElement>(json).ToString();
                var actualJson = JsonSerializer.Deserialize<JsonElement>(content).ToString();

                Assert.AreEqual(expectedJson, actualJson);
            } // end SaveAndClose()
        } // end inner class Valid
    } // end class
} // end namespace
