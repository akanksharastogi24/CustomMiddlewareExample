Basic Minimal API of Todo List, to test Custom Middleware Logger.

curl http://localhost:5000/api/todos
curl http://localhost:5000/api/todos/1
curl http://localhost:5000/api/todos/fail
curl http://localhost:5000/api/todos/slow

curl -X POST http://localhost:5000/api/todos -H "Content-Type: application/json" -d "{\"title\":\"Check in Git\"}"
