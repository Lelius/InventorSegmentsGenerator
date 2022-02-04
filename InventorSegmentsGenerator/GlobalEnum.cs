namespace InventorSegmentsGenerator
{
    /// <summary>
    /// Перечень профилей, используемых в этом приложении.
    /// </summary>
    public enum ProfileTypeEnum { A38, A48, AP48, AP58 };
    /// <summary>
    /// Варианты установки среднего прутка секции.
    /// </summary>
    public enum RodInMiddleEnum { Yes, No, Auto };
    /// <summary>
    /// Перечень вариантов нижних окончаний вертикальных стоек секции.
    /// </summary>
    /// <remarks>Boot - в металлический башмак, 
    /// Ground - в грунт с бетонированием, 
    /// Rack - безногий вариант.</remarks>
    public enum TypeOfFasteningEnum { Boot, Ground, Rack };
}