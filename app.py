from flask import Flask, jsonify, request

app = Flask(__name__)

tasks = []
next_id = 1

@app.route('/tasks', methods=['GET'])
def get_tasks():
    return jsonify(tasks)

@app.route('/tasks', methods=['POST'])
def add_task():
    global next_id
    data = request.json
    task = {'id': next_id, 'description': data['description']}
    tasks.append(task)
    next_id += 1
    return jsonify(task), 201

@app.route('/tasks/<int:id>', methods=['DELETE'])
def delete_task(id):
    global tasks
    tasks = [task for task in tasks if task['id'] != id]
    return '', 204

if __name__ == "__main__":
    app.run(host='0.0.0.0', port=5001, debug=True)

