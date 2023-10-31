#include <Windows.h>
#include <string>
#include <sstream>

// Идентификаторы элементов управления
enum {
    IDC_EDIT1 = 1001,
    IDC_EDIT2,
    IDC_BUTTON_ADD,
    IDC_BUTTON_SUBTRACT,
    IDC_BUTTON_MULTIPLY,
    IDC_BUTTON_DIVIDE,
    IDC_RESULT
};

// Обработчик сообщений
LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    const wchar_t CLASS_NAME[] = L"MyWinAppClass";
    const wchar_t WINDOW_TITLE[] = L"Calculator";

    // Регистрация класса окна
    WNDCLASS wc = { 0 };
    wc.lpfnWndProc = WndProc;
    wc.hInstance = hInstance;
    wc.hCursor = LoadCursor(NULL, IDC_ARROW);
    wc.lpszClassName = CLASS_NAME;
    RegisterClass(&wc);

    // Создание окна
    HWND hwnd = CreateWindow(
        CLASS_NAME,
        WINDOW_TITLE,
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT, 400, 200,
        NULL,
        NULL,
        hInstance,
        NULL
    );

    if (hwnd == NULL)
    {
        MessageBox(NULL, L"Failed to create the window", L"Error", MB_OK | MB_ICONERROR);
        return 0;
    }

    // Создание полей ввода и кнопок
    CreateWindowEx(
        0, L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER,
        20, 20, 150, 20, hwnd, (HMENU)IDC_EDIT1, hInstance, NULL
    );

    CreateWindowEx(
        0, L"EDIT", NULL, WS_CHILD | WS_VISIBLE | WS_BORDER,
        20, 50, 150, 20, hwnd, (HMENU)IDC_EDIT2, hInstance, NULL
    );

    CreateWindow(
        L"BUTTON", L"Add", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        200, 20, 80, 20, hwnd, (HMENU)IDC_BUTTON_ADD, hInstance, NULL
    );

    CreateWindow(
        L"BUTTON", L"Subtract", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        200, 50, 80, 20, hwnd, (HMENU)IDC_BUTTON_SUBTRACT, hInstance, NULL
    );

    CreateWindow(
        L"BUTTON", L"Multiply", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        290, 20, 80, 20, hwnd, (HMENU)IDC_BUTTON_MULTIPLY, hInstance, NULL
    );

    CreateWindow(
        L"BUTTON", L"Divide", WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
        290, 50, 80, 20, hwnd, (HMENU)IDC_BUTTON_DIVIDE, hInstance, NULL
    );

    CreateWindowEx(
        0, L"EDIT", NULL, WS_CHILD | WS_VISIBLE | ES_READONLY | WS_BORDER,
        20, 100, 350, 20, hwnd, (HMENU)IDC_RESULT, hInstance, NULL
    );

    // Отображение окна
    ShowWindow(hwnd, nCmdShow);
    UpdateWindow(hwnd);

    // Основной цикл сообщений
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    static HWND hEdit1, hEdit2, hResult;
    switch (msg)
    {
    case WM_CREATE:
        // Получение дескрипторов элементов управления
        hEdit1 = GetDlgItem(hwnd, IDC_EDIT1);
        hEdit2 = GetDlgItem(hwnd, IDC_EDIT2);
        hResult = GetDlgItem(hwnd, IDC_RESULT);
        return 0;

    case WM_COMMAND:
        switch (LOWORD(wParam))
        {
        case IDC_BUTTON_ADD:
        case IDC_BUTTON_SUBTRACT:
        case IDC_BUTTON_MULTIPLY:
        case IDC_BUTTON_DIVIDE:
        {
            // Получение значений из полей ввода
            wchar_t buffer1[512], buffer2[512];
            GetDlgItemText(hwnd, IDC_EDIT1, buffer1, sizeof(buffer1));
            GetDlgItemText(hwnd, IDC_EDIT2, buffer2, sizeof(buffer2));
            float num1, num2;
            std::wstringstream ss1(buffer1);
            std::wstringstream ss2(buffer2);
            ss1 >> num1;
            ss2 >> num2;

            // Вычисление результата
            double result;
            if (LOWORD(wParam) == IDC_BUTTON_ADD)
                result = num1 + num2;
            else if (LOWORD(wParam) == IDC_BUTTON_SUBTRACT)
                result = num1 - num2;
            else if (LOWORD(wParam) == IDC_BUTTON_MULTIPLY)
                result = num1 * num2;
            else if (LOWORD(wParam) == IDC_BUTTON_DIVIDE)
                result = num1 / num2;

            // Преобразование результата в строку и установка в поле результата
            std::wstringstream ssResult;
            ssResult << result;
            SetDlgItemText(hwnd, IDC_RESULT, ssResult.str().c_str());
            return 0;
        }
        }
        break;

    case WM_DESTROY:
        PostQuitMessage(0);
        return 0;
    }

    return DefWindowProc(hwnd, msg, wParam, lParam);
}