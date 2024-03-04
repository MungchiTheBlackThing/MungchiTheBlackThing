namespace Assets.Script.TimeEnum
{
    public enum STimeIdx : int
    {
        SI_DAWN=0,
        SI_EVENING=1,
        SI_MORNING=2,
        SI_NIGHT=3,
    };
    public enum STime : int
    { //시작 시간
        T_MORNING=7, //7~16시까지
        T_EVENING=16, //16~17까지
        T_NIGHT=20, //20~3시까지
        T_DAWN=3, //3~7시까지
    };

    public enum TimeStamp : int
    {
        TS_WATCHING,
        TS_THINKING,
        TS_WRITING,
        TS_PLAY,
        TS_SLEEPING,
        TS_NEXTCHAPTER,
        TS_SIZE,
    };


    public enum Chapter : int
    {
        C_1DAY=1,
        C_2DAY,
        C_3DAY,
        C_4DAY,
        C_5DAY,
        C_6DAY,
        C_7DAY,
        C_8DAY,
        C_9DAY,
        C_10DAY,
        C_11DAY,
        C_12DAY,
        C_13DAY,
        C_14DAY,
        END
    };
}
