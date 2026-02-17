using ChipMasters.Controls;
using ChipMasters.GodotWrappers;
using Fakes.ChipMasters.GodotWrappers;

namespace Tests.ChipMasters.Controls
{
    public class TestNRegexTextEdit
    {
        [TestClass]
        public class Valid
        {
            [TestMethod]
            [DataRow("^[a-z]$", "a", false, "a", "a")]
            [DataRow("[a-z]", "a", false, "aadf", "a")]
            [DataRow("[a-z]", "a", false, "faadf", "f")]
            [DataRow("[0-9]*", "", false, "0 -9 ", "0")]
            [DataRow("[0-9]*", "", false, "0234 -9 ", "0234")]
            [DataRow("[0-9]*", "019", false, "01-9 ", "01")]

            [DataRow("[0-9]*", "019", true, "01-9 ", "019")]
            [DataRow("[0-9]*", "019", true, "01-9sklfk3242l2 ", "01932422")]
            public void Matching(string regex, string preText, bool allMatches, string postText, string expectation) // doesn't match string
            {
                ITextEdit t = new FakeTextEdit(text: preText);
                NRegexTextEdit.Constrain(t, regex, allMatches);
                t.Text = postText;
                Assert.AreEqual(expectation, t.Text);
            } // end Matching()
        } // end inner class Valid

        [TestClass]
        public class Invalid
        {
            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            [DataRow("^[a-z]$", "a", false, "")]
            [DataRow("^[a-z]$", "", false, "")]
            [DataRow("^[a-z]$", "a", false, "0")]
            [DataRow("^[a-z]$", "a", false, "0a")]

            [DataRow("^[a-z]$", "a", true, "")]
            [DataRow("^[a-z]$", "", true, "")]
            [DataRow("^[a-z]$", "a", true, "0")]
            [DataRow("^[a-z]$", "a", true, "0a")]
            public void NonMatching(string regex, string preText, bool allMatches, string postText) // doesn't match string
            {
                ITextEdit t = new FakeTextEdit(text: preText);
                NRegexTextEdit.Constrain(t, regex, allMatches);
                t.Text = postText;
            } // end NonMatching()

        } // end inner class Valid
    } // end class
} // end namespace