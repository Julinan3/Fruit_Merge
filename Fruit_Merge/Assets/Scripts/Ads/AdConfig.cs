public static class AdConfig
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitId();
    public static string InterstitalAdUnitId => GetInterstitialAdUnitId();

    public static string RewardedVideoAdUnitId1 => GetRewardedVideoAdUnitId1();
    public static string RewardedVideoAdUnitId2 => GetRewardedVideoAdUnitId2();
    public static string RewardedVideoAdUnitId3 => GetRewardedVideoAdUnitId3();
    public static string RewardedVideoAdUnitId4 => GetRewardedVideoAdUnitId4();
    public static string RewardedVideoAdUnitId5 => GetRewardedVideoAdUnitId5();
    public static string RewardedVideoAdUnitId6 => GetRewardedVideoAdUnitId6();
    public static string RewardedVideoAdUnitId7 => GetRewardedVideoAdUnitId7();
    /*
    bomb
    shrink
    blast
    up
    switch
    endgame2x
    dailySpin2x
    */

    static string GetAppKey()
    {
#if UNITY_ANDROID
        return "23b1697a5";
#elif UNITY_IPHONE
            return "8545d445";
#else
            return "unexpected_platform";
#endif
    }

    static string GetBannerAdUnitId()
    {
#if UNITY_ANDROID
        return "5ewc3mmlzz5yhie6";
#elif UNITY_IPHONE
            return "iep3rxsyp9na3rw8";
#else
            return "unexpected_platform";
#endif
    }
    static string GetInterstitialAdUnitId()
    {
#if UNITY_ANDROID
        return "4fplb2wfxs1jnk5w";
#elif UNITY_IPHONE
            return "wmgt0712uuux8ju4";
#else
            return "unexpected_platform";
#endif
    }

    static string GetRewardedVideoAdUnitId1()
    {
#if UNITY_ANDROID
        return "rawhye9em3ns82tt";
#elif UNITY_IPHONE
            return "rewarded_id_1_ios";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId2()
    {
#if UNITY_ANDROID
        return "qlg5ztarup808t8h";
#elif UNITY_IPHONE
            return "rewarded_id_2_ios";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId3()
    {
#if UNITY_ANDROID
        return "xuyfd7fwjgxsuwf1";
#elif UNITY_IPHONE
            return "rewarded_id_3_ios";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId4()
    {
#if UNITY_ANDROID
        return "gnzyhda5b7ocskhj";
#elif UNITY_IPHONE
            return "rewarded_id_4_ios";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId5()
    {
#if UNITY_ANDROID
        return "13d49tvlhymjw197";
#elif UNITY_IPHONE
            return "rewarded_id_5_ios";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId6()
    {
#if UNITY_ANDROID
        return "pi2ri7krzlzavk0k";
#elif UNITY_IPHONE
            return "rewarded_id_6_ios";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId7()
    {
#if UNITY_ANDROID
        return "ble7vs5rddwe5ute";
#elif UNITY_IPHONE
            return "rewarded_id_7_ios";
#else
            return "unexpected_platform";
#endif
    }
}