/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *    https://github.com/dashhoff
 *========================================================================
 */

public static class GameData 
{
    public static int CurrentTrack = 0;

    public static int TimeOfDay;
    
    public static void SetTrack(int value)
    {
        CurrentTrack = value;
    }

    public static void SetTimeOfDay(int value)
    {
        TimeOfDay = value;
    }
}
