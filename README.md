# Лабораторная работа 5: Построение AST и проверка контекстно-зависимых условий

**Автор:** Соболев Илья Олегович

---

## Вариант задания

**Тема работы:** Форматирование научной нотации в число с плавающей точкой на языке Python.

**Формат строки:**
```python
float_format = "{:f}".format(3.234e+4);

Примеры верных строк:
float = "{:f}".format(1e+4);

float_format = "{:f}".format(3.234e+4);

result = "{:f}".format(3.234555e+4);
```
**Контекстно-зависимые условия**

В лабораторной работе реализованы следующие проверки контекстно-зависимых условий:

| № | Проверка | Описание | Пример ошибки | Ожидаемое сообщение |
|---|----------|----------|---------------|---------------------|
| 1 | Уникальность имён | Запрет повторного объявления переменной с тем же именем в одной области видимости | `float x = 1; float x = 2;` | `"Variable 'x' already declared"` |
| 2 | Совместимость типов | Проверка соответствия типов при присваивании и передаче аргументов | `int x = "{:f}".format(3e+4);` | `"Type mismatch: expected String, got FormatCallNode"` |
| 3 | Допустимые значения | Проверка корректности формата научной нотации (мантисса, экспонента) | `float_format = "{:f}".format(1e+999);` | `"Exponent value out of valid range"` |
| 4 | Использование объявленных идентификаторов | Запрет использования переменных до их объявления | `result = format(x);` (если x не объявлен) | `"Variable 'x' is not declared"` |
| 5 | Корректный specifier форматирования | Проверка, что specifier является допустимым для чисел с плавающей точкой | `float_format = "{:d}".format(3.14e+4);` | `"Invalid format specifier for float: expected '{:f}'"` |
| 6 | Наличие обязательных компонентов | Проверка наличия и точки, и форматной строки, и аргумента | `float_format = .format(3e+4);` | `"Missing required format string component"` |

**Структура AST для верной строки**

| Уровень | Тип узла | Поле | Значение |
|---------|----------|------|----------|
| 0 | `ProgramNode` | statements | `[AssignmentNode]` |
| 1 | `AssignmentNode` | name | `"float_format"` |
| 1 | `AssignmentNode` | type | `StringNode` |
| 1 | `AssignmentNode` | value | `FormatCallNode` |
| 2 | `FormatCallNode` | specifier | `"{:f}"` |
| 2 | `FormatCallNode` | argument | `ScientificNumberNode` |
| 3 | `ScientificNumberNode` | mantissa | `3.234` |
| 3 | `ScientificNumberNode` | exponent | `4` |
| 3 | `ScientificNumberNode` | sign | `+` |


**Дерево CST**
<img width="453" height="408" alt="CST_дерево" src="https://github.com/user-attachments/assets/10228b97-5051-4c14-9160-8e19bb7c39ef" />



**Формат вывода AST в программе**

<img width="412" height="218" alt="image" src="https://github.com/user-attachments/assets/272c9851-2038-4f4f-a42e-9ea5a024817d" />


**Тестовые примеры:**

Тестовый пример верный:
<img width="1328" height="595" alt="Пример_верный" src="https://github.com/user-attachments/assets/226399a7-950e-4497-b862-505ef8b6a1f5" />


Тестовый пример неверный:
<img width="1391" height="428" alt="Пример_неверный1" src="https://github.com/user-attachments/assets/9e61c0b9-71f6-4ec9-ab05-45a3970d1ca7" />


Тестовый пример неверный:
<img width="1313" height="736" alt="Пример_неверный2" src="https://github.com/user-attachments/assets/a8aeaa3b-20fc-43c6-94cb-36180ebd652a" />
