class chapterAbility
{
    public string ID;
    public int AbilityNumber;//哪一個屬性
    public int AbilityLevel;//等級
    public int Adjust;//調整上升
    public chapterAbility(string chapterID, int number, int Level)
    {
        this.ID = chapterID;
        this.AbilityNumber = number;
        this.AbilityLevel = Level;
        this.Adjust = 0;
    }

    public chapterAbility()
    {
        this.ID = "";
        this.AbilityLevel = 99;
        this.AbilityNumber = 0;
    }
}