namespace MultimeterApp.Model
{
    // Настройка порядка режимов настраивается тут, нужно просто повторить порядок (или присвоить нужные индексы),
    // в котором стоят режимы на мультиметре.
    // Можно было сделать разными способами, но я решил сделать по простому.
    public enum MultimeterMode
    {
        Neutral,
        DCVoltage,
        Current,
        ACVoltage,
        Resistance,
    }
}