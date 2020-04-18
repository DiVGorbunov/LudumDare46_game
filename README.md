# Документация

## Добавления условия проигрыша

Для поражения в игре были переписаны класс игровых целей Objective и менеджер этих объектов ObjectiveManager. Теперь Objective поддерживает флаг isFailing. Цели с положительным значением этого флага, могут завершить игру поражением.

Например, ObjectiveUnsatisfiedClients.

Для того, чтобы добавить такую цель:
1. убедитесь, что на вашей сцене присутствует GameFlowManager из FPS;
2. добавление целей происходит подобным образом, как добавление цели ObjectiveKillEnemies:
2.1. создайте пустой объект на сцене;
2.2. добавьте к нему компонент Objective с флагом isFailing = true;
2.3. добавьте к нему компонент ObjectiveUnsatisfiedClients (ClientManager для него находится в коде и не требует настройки);
2.4. (необязательно) - добавьте к нему компонент Display Message

Такая цель будет публиковать нотификации ObjectiveHUDManger и NotificationHUDManager.