using ChipMasters.User;
using ChipMasters.Util;
using Godot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Avatars
{
    /// <summary>
    /// Edits <see cref="RUser.INSTANCE"/>'s <see cref="IUser.Avatar"/>, 
    /// </summary>
    public partial class NAvatarEditor : Node
    {
        /// <summary>
        /// Randomized indexs of after options to assure balanced distribution and no bias towards certain traits being presented first.
        /// </summary>
        public static readonly ImmutableDictionary<string, ImmutableList<int>> SHUFFLED_TEXTURE_INDEXS;

        static NAvatarEditor()
        {
            Random r = new();
            SHUFFLED_TEXTURE_INDEXS =
                NAvatarDisplay.TEXTURES
                .Select((KeyValuePair<string, ImmutableList<Texture2D>> x)
                    => KeyValuePair.Create(x.Key, Enumerable.Range(0, x.Value.Count).ToList().Shuffled<List<int>, int>().ToImmutableList()))
                .ToImmutableDictionary();
#if DEBUG
            foreach (KeyValuePair<string, ImmutableList<int>> kvp in SHUFFLED_TEXTURE_INDEXS)
            {
                GD.Print(kvp.Key);
                foreach (int i in kvp.Value)
                    GD.Print(i);

                GD.Print();
            }
#endif
        } // end cctor

        [Export] private Button _backgroundCycleButton = null!;
        [Export] private Button _skinCycleButton = null!;
        [Export] private Button _hairCycleButton = null!;
        [Export] private Button _eyesCycleButton = null!;
        [Export] private Button _mouthCycleButton = null!;
        [Export] private Button _randomizeButton = null!;
        [Export] private LineEdit _nameInput = null!;



        /// <summary>
        /// Initializes the avatar customization system when the node is ready.
        /// </summary>
        public override void _Ready()
        {
            _backgroundCycleButton.AssertNotNull().Pressed += () => CycleTexture("background");
            _skinCycleButton.AssertNotNull().Pressed += () => CycleTexture("skin");
            _hairCycleButton.AssertNotNull().Pressed += () => CycleTexture("hair");
            _eyesCycleButton.AssertNotNull().Pressed += () => CycleTexture("eyes");
            _mouthCycleButton.AssertNotNull().Pressed += () => CycleTexture("mouth");
            _randomizeButton.AssertNotNull().Pressed += () => NAvatarDisplay.RandomizeAvatar(RUser.INSTANCE.Avatar);
            _nameInput.AssertNotNull().TextChanged += (string newText) => RUser.INSTANCE.Avatar.Name = newText;
        } // end _Ready()


        /// <summary>
        /// Cycles to the next texture for a specified avatar part.
        /// </summary>
        /// <param name="part">The name of the avatar part to cycle</param>
        private void CycleTexture(string part)
        {
            if (SHUFFLED_TEXTURE_INDEXS[part].Count == 0)
                return;

            RAvatar av = RUser.INSTANCE.Avatar;
            av.SetPart(part, SHUFFLED_TEXTURE_INDEXS[part][(SHUFFLED_TEXTURE_INDEXS[part].IndexOf(av.Parts[part]) + 1) % SHUFFLED_TEXTURE_INDEXS[part].Count]);
        } // end CycleTexture()
    } // end class
} // end namespace