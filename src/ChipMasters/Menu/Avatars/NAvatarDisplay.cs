using ChipMasters.GodotWrappers;
using ChipMasters.GodotWrappers.Helpers;
using ChipMasters.User;
using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Avatars
{
    /// <summary>
    /// Displays <see cref="RUser.INSTANCE"/>'s <see cref="IUser.Avatar"/>, 
    /// contains also static methods for creating random avatars, and static fields containing possible different names and textures..
    /// </summary>
    public partial class NAvatarDisplay : NNode
    {
        /// <summary>
        /// List of poker and blackjack themed names for random name generation
        /// </summary>
        public static readonly IReadOnlyList<string> RANDOM_NAMES = new string[]
        {
		// Poker terms
		"Ace", "Bluff", "Chip", "Dealer", "Flush", "Holdem", "Poker", "Royal", "Shark", "Stack",
        "Allin", "Blind", "Check", "Fold", "Raise", "River", "Turn", "Flop", "Pocket", "Suited",
        "Unsuited", "Draw", "Nuts", "Tilt", "Muck", "Showdown", "Bubble", "Bounty", "Satellite", "Turbo",
		// Blackjack terms
		"Hit", "Stand", "Split", "Double", "Bust", "Blackjack", "King", "Queen", "Jack", "Ten",
        "Soft", "Hard", "Push", "Shoe", "Cut", "Insurance", "Surrender", "Natural", "House", "Bank",
        "Table", "Bet", "Wager", "Count", "Shuffle", "Deck"
        };

        /// <summary>
        /// Dictionary by <see cref="RAvatar"/> part key to all possible textures for that given part.
        /// </summary>
        public static readonly ImmutableDictionary<string, ImmutableList<Texture2D>> TEXTURES;

        static NAvatarDisplay()
        {
            TEXTURES =
                RAvatar.PART_KEYS.Select((string key) => KeyValuePair.Create(
                    key,
                    LoadTextures($"res://assets/_/textures/avatar/{key}/")))
                .ToImmutableDictionary();
        } // end cctor()

        /// <summary>
        /// Loads all PNG textures from a specified folder path.
        /// </summary>
        /// <param name="folderPath">The path to the folder containing textures</param>
        /// <returns>A list of loaded textures</returns>
        private static ImmutableList<Texture2D> LoadTextures(string folderPath)
        {
            List<Texture2D> loadedTextures = new();
            DirAccess dir = DirAccess.Open(folderPath);

            if (dir is not null)
            {
                dir.ListDirBegin();
                string fileName;
                while ((fileName = dir.GetNext()) != "")
                    if (fileName.EndsWith(".png"))
                    {
                        var texture = GD.Load<Texture2D>(folderPath + fileName);
                        if (texture is not null)
                            loadedTextures.Add(texture);
                    }

                dir.ListDirEnd();
            }
            return loadedTextures.ToImmutableList();
        } // end LoadTextures()



        [Export] private TextureRect _backgroundTextureRect = null!;
        [Export] private TextureRect _skinTextureRect = null!;
        [Export] private TextureRect _hairTextureRect = null!;
        [Export] private TextureRect _eyesTextureRect = null!;
        [Export] private TextureRect _mouthTextureRect = null!;
        [Export] private Node _nameDisplay = null!;

        private readonly Dictionary<string, TextureRect> _textureRects = new();




        /// <inheritdoc/>
        public override void _Ready()
        {
            base._Ready();
            _textureRects[RAvatar.BACKGROUND_KEY] = _backgroundTextureRect.AssertNotNull();
            _textureRects[RAvatar.SKIN_KEY] = _skinTextureRect.AssertNotNull();
            _textureRects[RAvatar.HAIR_KEY] = _hairTextureRect.AssertNotNull();
            _textureRects[RAvatar.EYES_KEY] = _eyesTextureRect.AssertNotNull();
            _textureRects[RAvatar.MOUTH_KEY] = _mouthTextureRect.AssertNotNull();
            _nameDisplay.AssertNotNull();

            RUser.INSTANCE.Avatar.OnChanged += RefreshDisplay;
            RefreshDisplay();
        } // end _Ready()

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            RUser.INSTANCE.Avatar.OnChanged -= RefreshDisplay;
        } // end Dispose()



        private void RefreshDisplay()
        {
            RAvatar av = RUser.INSTANCE.Avatar;
            foreach (KeyValuePair<string, TextureRect> kvp in _textureRects)
                kvp.Value.Texture = TEXTURES[kvp.Key][av.Parts[kvp.Key]];

            ITextual nd = (ITextual)_nameDisplay;
            if (nd.Text != av.Name)
                nd.Text = av.Name;
        } // end RefreshDisplay()



        /// <summary>
        /// Randomizes all avatar parts and the name.
        /// </summary>
        public static RAvatar RandomAvatar()
        {
            Random random = new();
            return new RAvatar(
                RAvatar.PART_KEYS.Select((string x) => KeyValuePair.Create(x, random.Next(TEXTURES[x].Count))),
                name: RandomName());
        } // end RandomAvatar()

        /// <summary>
        /// Fully randomize an <see cref="RAvatar"/>s parts and name.
        /// </summary>
        /// <param name="avatar"></param>
        public static void RandomizeAvatar(RAvatar avatar)
        {
            Random random = new();
            foreach (string partKey in RAvatar.PART_KEYS)
                avatar.SetPart(partKey, random.Next(TEXTURES[partKey].Count));

            avatar.Name = RandomName();
        } // end RandomizeAvatar()

        /// <summary>
        /// Generates a random name with optional digits before, after, or both.
        /// </summary>
        /// <returns>A randomly generated name with optional digits</returns>
        private static string RandomName()
        {
            Random random = new();
            string baseName = RANDOM_NAMES[random.Next(RANDOM_NAMES.Count)];

            // Randomly decide if we'll add digits (75% chance)
            if (random.Next(4) == 0)
                return baseName;

            // Randomly decide how many digits to add (1-4)
            string digits = Enumerable.Range(0, random.Next(1, 5))
                .Select((x) => random.Next(10))
                .Aggregate("", (seed, x) => seed + x);

            // Randomly decide where to put the digits
            switch (random.Next(3))
            {
                case 0: return digits + baseName;  // Before
                case 1: return baseName + digits;  // After
                default:  // Both
                    int beforeDigits = random.Next(1, digits.Length);
                    int afterDigits = digits.Length - beforeDigits;
                    return string.Concat(digits.AsSpan(0, beforeDigits), baseName, digits.AsSpan(beforeDigits, afterDigits));
            }
        } // end RandomName()
    } // end class
} // end namespace