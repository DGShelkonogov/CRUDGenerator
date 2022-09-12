using ComicShop.Models;
using ScripGenerateCRUDSForAvalonia;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows.Input;
//бага #1 - если обьект содержит в себе несколько других (obj) обьектов ожинакого типа , то коллекция содержащая в себе все экземпляры дублируется 
//фиксить это пока не буду, из-за возможности легкого исправления ручками в коде и редкости случая

GenerateViewModel.create(new Comic());

