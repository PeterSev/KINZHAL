﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KINZHAL
{
    public enum DEVICE_ADDR
    {
        СУО         =36,
        ПКП_ПУ2     =37,
        ПУ_К        =40,
        ПК_К        =32,
        ПУ_НР       =39,
        ПК_НР       =31,
        ПУ_РД       =38,
        ПК_РД       =30,
        БУ          =21,
        УСО         =34,
        ППК         =35,
        ВСЕМ        =63
    };

    public enum DESCRIPTORS
    {
        Захват_управления_устройством = 256,
        Освобождение_захваченного_устройства = 257,
        Запрос_абонента = 1,
        Установить_оптический_фильтр = 312,
        Изменить_режим_работы = 320,
        Перейти_в_стабилизацию = 328,
        Сопровождение_по_углам_и_скоростям_1 = 336,
        Сопровождение_по_углам_и_скоростям_2 = 344,
        Сопровождение_по_углам_и_скоростям_3 = 352,
        Сопровождение_по_углам_и_скоростям_4 = 360,
        Сопровождение_по_скоростям_1 = 368,
        Сопровождение_по_скоростям_2 = 376,
        Сопровождение_по_скоростям_3 = 384,
        Переброс_по_заданным_углам_1 = 392,
        Переброс_по_заданным_углам_2 = 400,
        Переброс_по_заданным_углам_3 = 408,
        Запрос_диагностики = 416,
        Сменить_поле_зрения_прицела = 424,
        Измерить_дальность = 432,
        Включить_обогрев_стекла = 440,
        Запросить_наработку_ЛД = 448,
        Запросить_наработку_прицела = 456,
        Провести_калибровку_ТПВ_матрицы = 464,
        Перейти_в_транспортное_положение = 472,
        Запросить_параметр = 480,
        Включить_ТПВ_матрицу = 488,
        Отключить_ТПВ_матрицу = 496,
        Запросить_координаты_визирной_оси = 504,


        Причина_старта_абонента_ППК = 3,
        Команда_принята_к_исполнению = 1280,
        Команда_не_принята_к_исполнению = 1281,
        Команде_не_поддерживается_абонентом = 1282,
        Команда_не_может_быть_выполнена_Абонент_занят = 1283,
        Команда_не_может_быть_выполнена_в_данном_режиме_работы_абонента = 1284,
        Ошибка_в_параметрах_команды = 1285,
        Команда_не_может_быть_выполнена_Абонент_захвачен = 1286,
        Отклик_абонента = 2,
        Состояние_прицела_1 = 1538,
        Состояние_прицела_2 = 1546,
        Состояние_прицела_3 = 1554,
        Команда_исполнена = 1562,
        Код_ошибки = 1570,
        Наработка_ЛД = 1578,
        Измеренная_дальность = 1586,
        Время_измерения_ЛД = 1594,
        Диагностика = 1602,
        Значение_параметра = 1610,
        Координаты_визирной_оси_1 = 1618,
        Координаты_визирной_оси_2 = 1626,
        Координаты_визирной_оси_3 = 1634,
        Координаты_визирной_оси_4 = 1642

        /*Выбор_параметра_для_изменения_его_значения = 303,
        Регулировка_плюс = 304,
        Регулировка_минус = 307,
        Установка_регулируемого_параметра = 308,
        Выбрать_дальность = 309,
        Индикация_служебной_информации = 310,
        Циклическая_смена_канала_поля_зрения_и_увеличения = 311,
        
        Светофильтр = 313,
        Обогрев = 314,
        Меню = 315,
        Ручной_ввод_дальности = 316,
        Вверх = 371,
        Вниз = 372,
        Влево = 373,
        Вправо = 374,
        Выбор_пункта_меню_ПКП_МРО = 375,
        Состояние_клавиш_ПКП_ПУ = 356,
        Установить_строб = 319,
        
        Позитив_Негатив_ТПВК = 321,
        Калибровка_ТПВК = 324,
        Передать_наработку_ДК = 328,
        Выключить_ПКП_МРО = 358,
        Ввод_Д_У = 359,
        Управление_наведением_ПКП_МРО_по_углу = 361,
        Управление_наведением_ПКП_МРО_по_скорости = 362,
        Сформировать_целеуказание_для_БУ = 333,
        Отработать_ВЦУ = 334,
        Отменить_ВЦУ = 336,
        Компенсация_увода_ПКП_МРО = 338,
        Передача_углов_ЦУ_в_ПКП_БУ = 337,
        Фокусировка_плюс = 380,
        Фокусировка_минус = 381,
        Запросить_время_работы_камеры_ТПВК = 349,
        Запросить_время_работы_охладителя_ТПВК = 353,
        Подсветка_лазером = 354,
        Фиксация_скорости = 355,
        
        Передача_углов_целеуказания_для_ПКП_БУ = 370,
        Состояние_ПКП_МРО = 1539,
        Дальность = 363,
        

        

        

        Наработка_ДК = 365,
        Текущая_дальность_и_строб = 1366,
        
        Время_работы_камеры = 367,
        
        Передача_углов_целеуказания_в_БУ = 299,
        Угол_наведения_и_угловая_скорость_МТТД_по_горизонту = 1540*/
    };

    public enum typeMessage { SERVICE, COMMAND, ACK, BROADCAST, OTHER };

    public enum DESCR_CAN_STATUS
    {
        No_Error = 0,
        Transmit_buffer_of_the_CAN_controller_is_full = 1,
        Receive_buffer_of_the_CAN_controller_is_full = 2,
        Bus_error_Error_Limit_1_exceeded_Warning_Limit_reached = 4,
        Bus_error_Error_Limit_2_exceeded_Error_Passive = 8,
        Bus_error_CAN_controller_has_gone_into_Bus_Off_state = 16,
        No_CAN_message_is_within_the_receive_buffer = 32,
        Receive_buffer_is_full_CAN_messages_has_been_lost = 64,
        Transmit_buffer_is_full = 128,
        Register_test_of_the_CAN_controller_failed = 256,
        Memory_test_on_hardware_failed = 512,
        Transmit_CAN_message_was_automatically_deleted_by_firmware_Transmit_timeout = 1024,
    };
}
