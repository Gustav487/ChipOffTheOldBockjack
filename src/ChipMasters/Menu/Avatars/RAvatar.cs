using GSR.EnDecic;
using GSR.EnDecic.EnDecs;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ChipMasters.Menu.Avatars
{
    /// <summary>
    /// Object representing a users avatar based on selected options.
    /// </summary>
    public sealed class RAvatar
    {
        /// <summary>
        /// Key to the avatar' background, used in encoding/decoding and tracking options.
        /// </summary>
        public const string BACKGROUND_KEY = "background";
        /// <summary>
        /// Key to the avatar' skin, used in encoding/decoding and tracking options.
        /// </summary>
        public const string SKIN_KEY = "skin";
        /// <summary>
        /// Key to the avatar' hair, used in encoding/decoding and tracking options.
        /// </summary>
        public const string HAIR_KEY = "hair";
        /// <summary>
        /// Key to the avatar' eys, used in encoding/decoding and tracking options.
        /// </summary>
        public const string EYES_KEY = "eyes";
        /// <summary>
        /// Key to the avatar's mouth, used in encoding/decoding and tracking options.
        /// </summary>
        public const string MOUTH_KEY = "mouth";

        /// <summary>
        /// All valid avatar part keys.
        /// </summary>
        public static readonly IImmutableList<string> PART_KEYS = new string[] { BACKGROUND_KEY, SKIN_KEY, HAIR_KEY, EYES_KEY, MOUTH_KEY }
            .ToImmutableList();

        /// <summary>
        /// <see cref="IEnDec{T}"/> for coding <see cref="RAvatar"/>s.
        /// </summary>
        public static readonly IEnDec<RAvatar> ENDEC = EnDecUtil.KeyedEnDecBuilder<string, RAvatar>(EnDecUtil.STRING)
            .Add("parts", EnDecUtil.INT_32.Ranged(0, int.MaxValue)
                .FixedKeyMapOf(
                    EnDecUtil.STRING,
                    PART_KEYS.ToArray()),
                (RAvatar x) => x._parts)
            .Add("name", EnDecUtil.STRING, (RAvatar x) => x.Name)
            .Build((p, n) => new RAvatar(p, n));



        /// <summary>
        /// Avatar's parts by their <see cref="PART_KEYS"/>.
        /// </summary>
        public IReadOnlyDictionary<string, int> Parts => _parts;
        private readonly Dictionary<string, int> _parts;

        /// <summary>
        /// The user's 'name'.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnChanged?.Invoke();
            }
        }
        private string _name;
        /// <summary>
        /// Event raised anytime any part of the avatar information changes.
        /// </summary>
        public event Action? OnChanged;



        /// <inheritdoc/>
        public RAvatar()
            : this(PART_KEYS.Select((string key) => KeyValuePair.Create(key, 0)), "") { } // end ctor

        /// <inheritdoc/>
        public RAvatar(IEnumerable<KeyValuePair<string, int>> parts, string name)
        {
            _parts = new Dictionary<string, int>(parts.AssertNotNull().Select((x) => Validated(x.Key, x.Value)));
            _name = name.AssertNotNull();
        } // end ctor



        /// <summary>
        /// Change the selection for a give part.
        /// </summary>
        /// <param name="key">The part's key.</param>
        /// <param name="index">The new index.</param>
        public void SetPart(string key, int index)
        {
            Validated(key, index);
            _parts[key] = index;
            OnChanged?.Invoke();
        } // end SetPart()

        private KeyValuePair<string, int> Validated(string key, int index)
        {
            if (!PART_KEYS.Contains(key.AssertNotNull()))
                throw new ArgumentException();
            if (index < 0)
                throw new ArgumentOutOfRangeException();
            return KeyValuePair.Create(key, index);
        } // end Validated()

    } // end struct
} // end namespace