# Expense Management System Backend **Project - Local Setup Guide (Windows)**

This guide will walk you through the steps to set up the **Eexpense Management System Backend** in local.

## **Prerequisites**

Before you begin, ensure that you have the following prerequisites installed on your system:

-   **[Git](https://git-scm.com/downloads)**
-   **[.NET Core SDK](https://dotnet.microsoft.com/download)**
-   **[Node.js](https://nodejs.org/en/download/)**
-   **[PostgreSQL](https://www.postgresql.org/download/windows/)**

## **Step 1: Clone the Repository**

1. Open a command prompt or Git Bash.
2. Change to the directory where you want to clone the project.
3. Execute the following command to clone the ABP.IO repository:

```
https://github.com/RahuII/expense-management-system-backend.git
```



## **Step 2: Install ABP CLI**

ABP.IO provides a command-line interface (CLI) tool, ABP CLI, to simplify project creation and management. To install the ABP CLI, follow these steps:

1. Open a command prompt or Git Bash.
2. Execute the following command to install the ABP CLI globally:

    ```
    dotnet tool install -g Volo.Abp.Cli
    ```

    Note: If you have previously installed ABP CLI, you can update it by executing **`dotnet tool update -g Volo.Abp.Cli`**.

3. Open a command prompt or Git Bash.
4. Change to the project's root directory.
5. Execute the following command to create the database schema:
6. Checkout to development branch
   
    ```
    git checkout dev
    ```

    ```
    dotnet ef database update --project src/EMS.EntityFrameworkCore
    ```
## **Step 3: Install Project Library**
- Open your project in terminal and run ```abp install-libs```
## **Step 4: Update connection string of ```appsettings.json``` file:** 
- Open the `.\src\EMS.HttpApi.Host` folder and change your private connection string file.

## **Step 5: Run the Application**

1. Open a command prompt or Git Bas.
2. Change to the project's root directory.
3. Execute the following command to run the application:

    ```
    dotnet run --project src/EMS.HttpApi.Host
    ```

4. Open your web browser and navigate to **`https://localhost:44341`** (default URL).
5. Congratulations! You should see the Saturn project's swagger page.
