using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Constants
{
    public class StorageConstants
    {
        public const string PLAYER_SCORE_KEY = "playerScoreKey";
        public const string PLAYER_NAME_KEY = "playerNameKey";

        public static string GetKeyByPlayerPrefs(PlayerPrefsEnum playerPrefs)
        {
            return playerPrefs switch
            {
                PlayerPrefsEnum.PLAYER_NAME => PLAYER_NAME_KEY,
                PlayerPrefsEnum.PLAYER_SCORE => PLAYER_SCORE_KEY,
                _ => throw new NotImplementedException()
            };
        }

        public enum PlayerPrefsEnum
        {
            PLAYER_SCORE,
            PLAYER_NAME
        }
    }
}
