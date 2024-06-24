﻿
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;



        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        public int prevLevel = 1;
        public int Level = 1;
        public string ProgressData;//
        public int FirstTime=0;
        public int countBlocks;
        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

            // Допустим, задать значения по умолчанию для отдельных элементов массива
           
        }
    }
}
