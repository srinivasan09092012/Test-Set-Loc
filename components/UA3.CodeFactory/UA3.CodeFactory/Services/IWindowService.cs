//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) {year}. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using System.Windows;

namespace UA3.CodeFactory.Services
{
    public interface IWindowService
    {
        MessageBoxResult ShowErrorMessage(string title, string errorMessage);
    }
}
