# TodoList Application

This repository contains a simple TodoList application with a frontend built using .NET MVC and a backend API built using Python Flask. The application allows users to create, read, update, and delete tasks.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 5.0 or later)
- [Python 3](https://www.python.org/downloads/)
- [pip](https://pip.pypa.io/en/stable/installation/)
- [virtualenv](https://virtualenv.pypa.io/en/latest/installation.html)
- [NGINX](https://www.nginx.com/resources/wiki/start/topics/tutorials/install/)
- Ubuntu machine for deployment

## Project Structure

- **TodoListMvcApp**: The .NET MVC frontend application
- **api**: The Python Flask backend API

## Getting Started

### 1. Setting up the Flask API

1. **Navigate to the `api` directory**:
    ```bash
    cd ~/Projects/TodoListApp/api
    ```

2. **Create and activate a virtual environment**:
    ```bash
    virtualenv venv
    source venv/bin/activate
    ```

3. **Install the dependencies**:
    ```bash
    pip install -r requirements.txt
    ```

4. **Run the Flask API**:
    ```bash
    python3 app.py
    ```

### 2. Setting up the .NET MVC Application

1. **Navigate to the `TodoListMvcApp` directory**:
    ```bash
    cd ~/Projects/TodoListApp/TodoListMvcApp
    ```

2. **Publish the .NET MVC Application**:
    ```bash
    dotnet publish -o publish
    ```

3. **Navigate to the `publish` directory**:
    ```bash
    cd publish
    ```

4. **Run the .NET MVC Application**:
    ```bash
    dotnet TodoListMvcApp.dll
    ```

### 3. Configuring NGINX

1. **Create an NGINX configuration file**:
    ```bash
    sudo nano /etc/nginx/sites-available/todolistapp
    ```

2. **Add the following configuration**:
    ```nginx
    server {
        listen 80;
        server_name 10.0.2.15;

        location / {
            proxy_pass http://localhost:5000;
            proxy_http_version 1.1;
            proxy_set_header Upgrade $http_upgrade;
            proxy_set_header Connection keep-alive;
            proxy_set_header Host $host;
            proxy_cache_bypass $http_upgrade;
        }
    }
    ```

3. **Create a symbolic link to enable the site**:
    ```bash
    sudo ln -s /etc/nginx/sites-available/todolistapp /etc/nginx/sites-enabled/
    ```

4. **Test the NGINX configuration**:
    ```bash
    sudo nginx -t
    ```

5. **Restart NGINX**:
    ```bash
    sudo systemctl restart nginx
    ```

### Accessing the Application

- **API**: `http://10.0.2.15:5001`
- **.NET MVC Application**: `http://10.0.2.15/Todo/`

### Notes

- Ensure the Flask API is running on port 5001.
- Ensure the .NET MVC Application is correctly configured to communicate with the Flask API on port 5001.

## Contributing

Feel free to submit issues or pull requests for any improvements.

## License

This project is licensed under the Owner Dharanitharan [MIT License].
