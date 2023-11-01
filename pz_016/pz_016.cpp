//  pz_016.cpp

#pragma region test2
//#include <Windows.h>
//#include <string>
//#include <sstream>
//
//// Идентификаторы элементов управления
//enum {
//    IDC_EDIT1 = 1001,
//    IDC_EDIT2,
//    IDC_BUTTON_ADD,
//    IDC_BUTTON_SUBTRACT,
//    IDC_BUTTON_MULTIPLY,
//    IDC_BUTTON_DIVIDE,
//    IDC_RESULT
//};
//
//// Обработчик сообщений
//LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
//
//int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
//{
//    const wchar_t CLASS_NAME[] = L"MyWinAppClass";
//    const wchar_t WINDOW_TITLE[] = L"Calculator";
//
//    // Регистрация класса окна
//    WNDCLASS wc = { 0 };
//    wc.lpfnWndProc = WndProc;
//    wc.hInstance = hInstance;
//    wc.hCursor = LoadCursor(NULL, IDC_ARROW);
//    wc.lpszClassName = CLASS_NAME;
//    RegisterClass(&wc);
//
//    // Создание окна
//    HWND hwnd = CreateWindow(
//        CLASS_NAME,
//        WINDOW_TITLE,
//        WS_OVERLAPPEDWINDOW,
//        CW_USEDEFAULT, CW_USEDEFAULT, 400, 200,
//        NULL,
//        NULL,
//        hInstance,
//        NULL
//    );
//
//    if (hwnd == NULL)
//    {
//        MessageBox(NULL, L"Failed to create the window", L"Error", MB_OK | MB_ICONERROR);
//        return 0;
//    }
//
//    // Создание полей ввода и кнопок
//    CreateWindowEx(
//        0, L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER,
//        20, 20, 150, 20, hwnd, (HMENU)IDC_EDIT1, hInstance, NULL
//    );
//
//    CreateWindowEx(
//        0, L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER,
//        20, 50, 150, 20, hwnd, (HMENU)IDC_EDIT2, hInstance, NULL
//    );
//
//    CreateWindow(
//        L"BUTTON", L"Add", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
//        200, 20, 80, 20, hwnd, (HMENU)IDC_BUTTON_ADD, hInstance, NULL
//    );
//
//    CreateWindow(
//        L"BUTTON", L"Subtract", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
//        200, 50, 80, 20, hwnd, (HMENU)IDC_BUTTON_SUBTRACT, hInstance, NULL
//    );
//
//    CreateWindow(
//        L"BUTTON", L"Multiply", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
//        290, 20, 80, 20, hwnd, (HMENU)IDC_BUTTON_MULTIPLY, hInstance, NULL
//    );
//
//    CreateWindow(
//        L"BUTTON", L"Divide", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
//        290, 50, 80, 20, hwnd, (HMENU)IDC_BUTTON_DIVIDE, hInstance, NULL
//    );
//
//    CreateWindowEx(
//        0, L"EDIT", NULL, WS_CHILD | WS_VISIBLE | ES_READONLY | WS_BORDER,
//        20, 100, 350, 20, hwnd, (HMENU)IDC_RESULT, hInstance, NULL
//    );
//
//    // Отображение окна
//    ShowWindow(hwnd, nCmdShow);
//    UpdateWindow(hwnd);
//
//    // Основной цикл сообщений
//    MSG msg;
//    while (GetMessage(&msg, NULL, 0, 0))
//    {
//        TranslateMessage(&msg);
//        DispatchMessage(&msg);
//    }
//
//    return (int)msg.wParam;
//}
//
//LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
//{
//    static HWND hEdit1, hEdit2, hResult;
//    switch (msg)
//    {
//    case WM_CREATE:
//        // Получение дескрипторов элементов управления
//        hEdit1 = GetDlgItem(hwnd, IDC_EDIT1);
//        hEdit2 = GetDlgItem(hwnd, IDC_EDIT2);
//        hResult = GetDlgItem(hwnd, IDC_RESULT);
//        return 0;
//
//    case WM_COMMAND:
//        switch (LOWORD(wParam))
//        {
//        case IDC_BUTTON_ADD:
//        case IDC_BUTTON_SUBTRACT:
//        case IDC_BUTTON_MULTIPLY:
//        case IDC_BUTTON_DIVIDE:
//        {
//            // Получение значений из полей ввода
//            wchar_t buffer1[512], buffer2[512];
//            GetDlgItemText(hwnd, IDC_EDIT1, buffer1, sizeof(buffer1));
//            GetDlgItemText(hwnd, IDC_EDIT2, buffer2, sizeof(buffer2));
//            float num1, num2;
//            std::wstringstream ss1(buffer1);
//            std::wstringstream ss2(buffer2);
//            ss1 >> num1;
//            ss2 >> num2;
//
//            // Вычисление результата
//            double result;
//            if (LOWORD(wParam) == IDC_BUTTON_ADD)
//                result = num1 + num2;
//            else if (LOWORD(wParam) == IDC_BUTTON_SUBTRACT)
//                result = num1 - num2;
//            else if (LOWORD(wParam) == IDC_BUTTON_MULTIPLY)
//                result = num1 * num2;
//            else if (LOWORD(wParam) == IDC_BUTTON_DIVIDE)
//                result = num1 / num2;
//
//            // Преобразование результата в строку и установка в поле результата
//            std::wstringstream ssResult;
//            ssResult << result;
//            SetDlgItemText(hwnd, IDC_RESULT, ssResult.str().c_str());
//            return 0;
//        }
//        }
//        break;
//
//    case WM_DESTROY:
//        PostQuitMessage(0);
//        return 0;
//    }
//
//    return DefWindowProc(hwnd, msg, wParam, lParam);
//}
#pragma endregion

#include <Windows.h>
#include <sstream>
#include "../pz_012Lib/Library.h"

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
void InitializeComponent(HWND hwnd, HINSTANCE hInstance);

HWND hTextBox1;
HWND hTextBox2;

HWND hButtonAdd;
HWND hButtonSubtract;
HWND hButtonMultiply;
HWND hButtonDivide;

HWND hTextBoxResult;

HBRUSH hBackgroundBrush;

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    /*
    char8_t c{ u8'l' };
    char16_t d{ u'l' };
    char32_t e{ U'o' };
    */

    wchar_t className[] = L"Win32App";
    wchar_t windowTitle[] = L"pz_016 (bad)";

    WNDCLASSEX wcex = { 0 };
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.hInstance = hInstance;
    wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
    wcex.lpszClassName = className;
    RegisterClassEx(&wcex);

    HWND hwnd = CreateWindowEx(
        0,
        className,
        windowTitle,
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT, 350, 400,
        NULL, NULL,
        hInstance,
        NULL
    );

    if (hwnd == NULL)
    {
        MessageBox(NULL, L"fatal eror - no window", L"eror", MB_OK | MB_ICONASTERISK);
        return 0;
    }

    InitializeComponent(hwnd, hInstance);

    ShowWindow(hwnd, nCmdShow);
    UpdateWindow(hwnd);

    MSG msg = { 0 };
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

void InitializeComponent(HWND hwnd, HINSTANCE hInstance)
{
    hBackgroundBrush = CreateSolidBrush(RGB(199, 255, 255));

    hTextBox1 = CreateWindowEx(
        0, L"EDIT", L"",
        WS_CHILD | WS_VISIBLE | WS_BORDER | ES_AUTOHSCROLL,
        // x y width height
        10, 10, 200, 25,
        hwnd, NULL, hInstance, NULL);

    hTextBox2 = CreateWindowEx(
        0, L"EDIT", L"",
        WS_CHILD | WS_VISIBLE | WS_BORDER | ES_AUTOHSCROLL,
        // x y width height
        10, 40, 200, 25,
        hwnd, NULL, hInstance, NULL);

    hButtonAdd = CreateWindow(
        L"BUTTON", L"+",
        WS_CHILD | WS_VISIBLE | WS_BORDER | BS_PUSHBUTTON,
        10, 70, 50, 25,
        hwnd,
        NULL, hInstance,
        NULL
    );

    hButtonSubtract = CreateWindow(
        L"BUTTON", L"-",
        WS_CHILD | WS_VISIBLE | WS_BORDER | BS_PUSHBUTTON,
        10, 100, 50, 25,
        hwnd,
        NULL, hInstance,
        NULL
    );

    hButtonMultiply = CreateWindow(
        L"BUTTON", L"*",
        WS_CHILD | WS_VISIBLE | WS_BORDER | BS_PUSHBUTTON,
        65, 70, 50, 25,
        hwnd,
        NULL, hInstance,
        NULL
    );

    hButtonDivide = CreateWindow(
        L"BUTTON", L"/",
        WS_CHILD | WS_VISIBLE | WS_BORDER | BS_PUSHBUTTON,
        65, 100, 50, 25,
        hwnd,
        NULL, hInstance,
        NULL
    );

    hTextBoxResult = CreateWindowEx(
        0, L"EDIT", L"",
        WS_CHILD | WS_VISIBLE,
        10, 130, 200, 25,
        hwnd, NULL, hInstance, NULL
    );
}

void iLoveOcei(HWND hwnd, HDC hdc)
{
    SetTextColor(hdc, RGB(0, 0, 255)); // Синий цвет

    // win size
    RECT rect;
    GetClientRect(hwnd, &rect);
    int width = rect.right - rect.left;
    int height = rect.bottom - rect.top;

    const wchar_t text[] = L"ОКЭИ";
    SIZE textSize;
    GetTextExtentPoint32(hdc, text, wcslen(text), &textSize);
    int x = (width - textSize.cx) / 2;
    int y = height / 2;

    TextOut(hdc, x, y, text, wcslen(text) << 1);
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch (msg)
    {
    case WM_CREATE: return 0;

    case WM_CLOSE:
    {
        DestroyWindow(hwnd);
    } return 0;

    case WM_DESTROY:
    {
        PostQuitMessage(0);
    } return 0;

    case WM_SIZE:
    {
        int width = LOWORD(lParam);
        int height = HIWORD(lParam);

        //  center aligment!!
        int posX = (width - 200) / 2;
        int posY = (height - 150) / 2;

        SetWindowPos(hTextBox1, NULL, posX, posY, 200, 25, SWP_NOZORDER);
        SetWindowPos(hTextBox2, NULL, posX, posY + 30, 200, 25, SWP_NOZORDER);

        SetWindowPos(hButtonAdd, NULL, posX, posY + 60, 50, 25, SWP_NOZORDER);
        SetWindowPos(hButtonSubtract, NULL, posX + 60, posY + 90, 50, 25, SWP_NOZORDER);
        SetWindowPos(hButtonMultiply, NULL, posX, posY + 120, 50, 25, SWP_NOZORDER);
        SetWindowPos(hButtonDivide, NULL, posX + 60, posY + 150, 50, 25, SWP_NOZORDER);

        SetWindowPos(hTextBoxResult, NULL, posX, posY + 180, 200, 25, SWP_NOZORDER);

    }

    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hwnd, &ps);

        FillRect(hdc, &ps.rcPaint, (HBRUSH)hBackgroundBrush);

        iLoveOcei(hwnd, hdc);

    } return 0;

    case WM_COMMAND:
    {
        if (HIWORD(wParam) == BN_CLICKED)
        {
            wchar_t text1[256];
            SendMessage(hTextBox1, WM_GETTEXT, (WPARAM)256, (LPARAM)text1);
            wchar_t text2[256];
            SendMessage(hTextBox2, WM_GETTEXT, (WPARAM)256, (LPARAM)text2);

            double num1, num2, r;

            std::wstringstream st1(text1);
            std::wstringstream st2(text2);
            st1 >> num1;
            st2 >> num2;

            if ((HWND)lParam == hButtonAdd)
            {
                //std::wstringstream temp;
                //temp << num1;

                //MessageBox(hwnd, temp.str().c_str(), L"add!", MB_OK | MB_ICONHAND);

                //r = pz_012Lib::Arithmetic::Add(num1, num2);
                r = num1 + num2;

                std::wstringstream temp2;
                temp2 << r;

                SendMessage(hTextBoxResult, WM_SETTEXT, (WPARAM)256, (LPARAM)temp2.str().c_str());

                //MessageBox(hwnd, temp2.str().c_str(), L"", MB_ABORTRETRYIGNORE | MB_ICONASTERISK);
            }
            else if ((HWND)lParam == hButtonSubtract)
            {
                //r = pz_012Lib::Arithmetic::Subtract(num1, num2);

                r = num1 - num2;

                std::wstringstream temp2;
                temp2 << r;

                SendMessage(hTextBoxResult, WM_SETTEXT, (WPARAM)256, (LPARAM)temp2.str().c_str());

            }
            else if ((HWND)lParam == hButtonMultiply)
            {
                //MessageBox(hwnd, L"* click *", L"*", MB_OK);

                //r = pz_012Lib::Arithmetic::Multiply(num1, num2);

                r = num1 * num2;

                std::wstringstream temp2;
                temp2 << r;

                SendMessage(hTextBoxResult, WM_SETTEXT, (WPARAM)256, (LPARAM)temp2.str().c_str());
            }
            else if ((HWND)lParam == hButtonDivide)
            {
                //r = pz_012Lib::Arithmetic::Divide(num1, num2);

                r = num1 / num2;

                std::wstringstream temp2;
                temp2 << r;

                SendMessage(hTextBoxResult, WM_SETTEXT, (WPARAM)256, (LPARAM)temp2.str().c_str());
            }
        }

    } return 0;
    }

    return DefWindowProc(hwnd, msg, wParam, lParam);
}
/*
    std::cout << "1+2 = ";
    std::cout << pz_012Lib::Arithmetic::Add(1, 2);
    std::cout << "\n2*2 = ";
    std::cout << pz_012Lib::Arithmetic::Multiply(2, 2);
    std::cout << "\n10-9.1 = ";
    std::cout << pz_012Lib::Arithmetic::Divide(10, 9.1);
    std::cout << "\n10/9.1 = ";
    std::cout << pz_012Lib::Arithmetic::Subtract(10, 9.1);
    std::cout << "\n10**3.5 = ";
    std::cout << pz_012Lib::Arithmetic::Pow(10, 3.5);
    std::cout << "\n∛9 = ";
    std::cout << pz_012Lib::Arithmetic::InversePow(9,3);
*/