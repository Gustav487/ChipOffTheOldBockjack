using ChipMasters.User;
using GSR.Utilic.Generic;
using System.Collections.Generic;


namespace ChipMasters.Registers
{
    /// <summary>
    /// Static class for defining and registering achievements.
    /// </summary>
    public static class SAchievementTypes
    {
        /// <summary>
        /// Achievement for playing one game.
        /// </summary>
        public static readonly IAchievement WELCOME_TO_BLACKJACK =
            new RAchievement("Welcome to Blackjack - Play your first game of Blackjack  -  ", SStats.GAMES_PLAYED, 1);

        /// <summary>
        /// Achievement for playing fifty games.
        /// </summary>
        public static readonly IAchievement BLACKJACK_ACE =
            new RAchievement("Blackjack Ace - Play 50 games of Blackjack  -  ", SStats.GAMES_PLAYED, 50);

        /// <summary>
        /// Achievement for playing 500 games.
        /// </summary>
        public static readonly IAchievement BLACKJACK_LEGEND =
            new RAchievement("Blackjack Legend - Play 500 games of Blackjack  -  ", SStats.GAMES_PLAYED, 500);



        /// <summary>
        /// Achievement for winning 10 games.
        /// </summary>
        public static readonly IAchievement DEALERS_INCONVENIENCE =
            new RAchievement("Dealer's Inconvenience - Win 10 hands  -  ", SStats.WINS, 10);

        /// <summary>
        /// Achievement for winning 50 games.
        /// </summary>
        public static readonly IAchievement DEALERS_NIGHTMARE =
            new RAchievement("Dealer's Nightmare - Win 50 hands  -  ", SStats.WINS, 50);

        /// <summary>
        /// Achievement for winning 100 games.
        /// </summary>
        public static readonly IAchievement DEALERS_DEMONS =
            new RAchievement("Dealer's Demons - Win 100 hands  -  ", SStats.WINS, 100);



        /// <summary>
        /// Achievement for losing 10 games.
        /// </summary>
        public static readonly IAchievement BAD_LUCK =
            new RAchievement("Bad Luck - Lose your first hand  -  ", SStats.LOSSES, 1);

        /// <summary>
        /// Achievement for losing 50 games.
        /// </summary>
        public static readonly IAchievement NOT_YOUR_DAY =
            new RAchievement("Not Your Day - Lose 50 hands  -  ", SStats.LOSSES, 50);

        /// <summary>
        ///  Achievement for losing 100 games.
        /// </summary>
        public static readonly IAchievement YOU_ARE_BAD =
            new RAchievement("These Things Happen - Lose 100 hands  -  ", SStats.LOSSES, 100);



        /// <summary>
        /// Achievement for busting 10 times.
        /// </summary>
        public static readonly IAchievement BUSTED =
            new RAchievement("Busted - Bust 10 times  -  ", SStats.BUSTS, 10);

        /// <summary>
        /// Achievement for busting 25 times.
        /// </summary>
        public static readonly IAchievement BUST_OFTEN =
            new RAchievement("Bust Often? - Bust 25 times  -  ", SStats.BUSTS, 25);

        /// <summary>
        /// Achievement for busting 50 times.
        /// </summary>
        public static readonly IAchievement BUST_MONARCH =
            new RAchievement("Bust Monarch - Bust 50 times  -  ", SStats.BUSTS, 50);



        /// <summary>
        /// Achievement for betting 1000000 chips all time.
        /// </summary>
        public static readonly IAchievement THE_GAMBLER =
            new RAchievement("The Gambler - Bet at least 1,000,000 chips across all games -  ", SStats.BETTED_AMOUNT, 1_000_000);



        /// <summary>
        /// Achievement for getting a blackjack.
        /// </summary>
        public static readonly IAchievement THE_21_ONE =
            new RAchievement("The 21 One - Get Blackjack 1 time  -  ", SStats.BLACKJACKS, 1);

        /// <summary>
        /// Achievement for getting 10 blackjacks.
        /// </summary>
        public static readonly IAchievement THE_21_TEN =
            new RAchievement("The 21 Ten - Get Blackjack 10 times  -  ", SStats.BLACKJACKS, 10);

        /// <summary>
        /// Achievement for getting 21 blackjacks.
        /// </summary>
        public static readonly IAchievement THE_21_TWENTY_ONE =
            new RAchievement("The 21 Twenty One - Get Blackjack 21 times  -  ", SStats.BLACKJACKS, 21);



        /// <summary>
        /// Achievement for forfeiting a game.
        /// </summary>
        public static readonly IAchievement WHERE_ARE_YOU_GOING =
            new RAchievement("Where Are You Going? - Forfeit 1 time  -  ", SStats.FORFEITS, 1);



        /// <summary>
        /// Achievement for winning while holding 4 cards.
        /// </summary>
        public static readonly IAchievement FOUR_CARD_CHAMP =
            new RAchievement("Four Card Champ - Win with 4 cards  -  ", SStats.FOUR_CARD_WINS, 1);

        /// <summary>
        /// Achievement for winning while holding 5 cards.
        /// </summary>
        public static readonly IAchievement FIVE_CARD_CHAMP =
            new RAchievement("Five Card Champ - Win with 5 cards  -  ", SStats.FIVE_CARD_WINS, 1);

        /// <summary>
        /// Achievement for winning while holding 6 cards.
        /// </summary>
        public static readonly IAchievement SIX_CARD_CHAMP =
            new RAchievement("Six Card Champ - Win with 6 cards  -  ", SStats.SIX_CARD_WINS, 1);



        /// <summary>
        /// Achievement for winning with a hand valued at '17' one times.
        /// </summary>
        public static readonly IAchievement SEVENTEEN_LUCK =
            new RAchievement("Seventeen Luck  - Win by standing on 17  -  ", SStats.STAND_SEVENTEEN_WINS, 1);

        /// <summary>
        /// Achievement for winning with a hand valued at '17' five times.
        /// </summary>
        public static readonly IAchievement SEVENTEEN_STAND =
            new RAchievement("Seventeen Stand - Win by standing on 17 five times  -  ", SStats.STAND_SEVENTEEN_WINS, 5);

        /// <summary>
        /// Achievement for winning with a hand valued at '17' seventeen times.
        /// </summary>
        public static readonly IAchievement SEVENTEEN_SQUARED =
            new RAchievement("Seventeen² - Win by standing on 17 seventeen times  -  ", SStats.STAND_SEVENTEEN_WINS, 17);



        /// <summary>
        /// Achievement for betting 5000 chips at once.
        /// </summary>
        public static readonly IAchievement RISK_TAKER =
            new RAchievement("Risk Taker - Bet $5,000 or more in a single round  -  ", SStats.HIGH_BETS, 1);



        /// <summary>
        /// Achievement for holding 1000 chips at once.
        /// </summary>
        public static readonly IAchievement CASH_IN_HAND =
            new RAchievement("Cash in Hand - Reach a balance of $100  -  ", SStats.MAX_BALANCE, 1000);

        /// <summary>
        /// Achievement for holding 5000 chips at once.
        /// </summary>
        public static readonly IAchievement FAT_WALLET =
            new RAchievement("Fat Wallet - Reach a balance of $1,000  -  ", SStats.MAX_BALANCE, 5000);

        /// <summary>
        /// Achievement for holding 10000 chips at once.
        /// </summary>
        public static readonly IAchievement MONEY_IN_YOUR_BANK =
            new RAchievement("Money in Your Bank - Reach a balance of $10,000  -  ", SStats.MAX_BALANCE, 10000);



        /// <summary>
        /// Achievement for unlocking all achievements
        /// </summary>
        public static readonly IAchievement ACHIEVER =
            new RAchievement("Achiever - Achieve all achievements  -  ", SStats.NULL, int.MaxValue);



        /// <summary>
        /// Register identifying all attainable achievments with a unique key
        /// </summary>
        public static readonly IBijectiveDictionary<string, IAchievement> REGISTER = new Dictionary<string, IAchievement>
        {
            { "welcome_to_blackjack",  WELCOME_TO_BLACKJACK},
            { "blackjack_ace",  BLACKJACK_ACE},
            { "blackjack_legend",   BLACKJACK_LEGEND},
            { "dealers_inconvenience",   DEALERS_INCONVENIENCE },
            { "dealers_nightmare",   DEALERS_NIGHTMARE },
            { "dealers_demons",   DEALERS_DEMONS },
            { "bad_luck",   BAD_LUCK },
            { "not_your_day",   NOT_YOUR_DAY},
            { "your_are_bad",  YOU_ARE_BAD },
            { "bust",  BUSTED },
            { "busted_often",  BUST_OFTEN},
            { "bust_monarch",  BUST_MONARCH},
            { "the_gambler",  THE_GAMBLER},
            { "the_twentyone_one",  THE_21_ONE },
            { "the_twentyone_ten",  THE_21_TEN},
            { "the_twentyone_twentyone",  THE_21_TWENTY_ONE},
            { "where_are_you_going",  WHERE_ARE_YOU_GOING},
            { "four_card_champ",  FOUR_CARD_CHAMP },
            { "five_card_champ",  FIVE_CARD_CHAMP},
            { "six_card_champ",  SIX_CARD_CHAMP },
            { "seventeen_luck",  SEVENTEEN_LUCK},
            { "seventeen_stand",  SEVENTEEN_STAND},
            { "seventeen_squared",  SEVENTEEN_SQUARED},
            { "risk_taker",  RISK_TAKER},
            { "cash_in_hand",  CASH_IN_HAND},
            { "fat_wallet",  FAT_WALLET},
            { "money_in_your_bank",  MONEY_IN_YOUR_BANK},
            { "achiever",  ACHIEVER}
        }.ToImmutableBijectiveDictionary();

    } // end class
} // end namespace