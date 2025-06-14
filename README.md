# 🛒 SuperMarket Management System

A Data Structures & Algorithms (DSA)-oriented supermarket management desktop application built using C# and Windows Forms. This project was created as part of our academic coursework to practically demonstrate the performance and time complexity of various sorting algorithms and data structures in a real-world use case.

---

## 🎯 Project Objective

- ✅ Visualize and compare sorting algorithms through real data
- ✅ Demonstrate how different data structures affect performance
- ✅ Offer a working CRUD-based management system for users and items

---

## 🧠 Core Concepts Covered

- 🔢 Sorting Algorithms:
  - Bubble Sort
  - Selection Sort
  - Insertion Sort
  - Merge Sort
  - Quick Sort

- 🧱 Data Structures:
  - Dynamic Array
  - Linked List
  - Binary Search Tree (BST)

- ⏱️ Time Complexity Evaluation:
  - Execution time is displayed after each sort
  - Helps understand real-time impact of algorithms and structures

---

## 🖥️ Application Modules

### 1. 👥 User Management
- Add, Edit, Delete users
- Sort users by **Name** or **NIC**
- Select sorting algorithm and data structure
- View sorting time in milliseconds

### 2. 📦 Item Management
- Add, Edit, Delete items (e.g., food, medicine, drinks)
- Includes fields like Item Code, Gross Amount, Net Amount, Expiry/Manufacture Dates
- Select data structure for item storage
- Demonstrate data structure access time

 ### 3. 🗃️ **Database Integration**
- Stores all user and item data using **SQLite**
- Lightweight embedded database – no external server needed
- Ensures data persistence between sessions

---

## 📸 Screenshots

> Here are a few sample screens to give an idea of the UI:

- User sorting based on selected algorithm and data structure
- Sorting time shown as feedback
- Item CRUD operations and storage options
- Category-based classification
![Screenshot (35)](https://github.com/user-attachments/assets/6dc5ee6e-08be-47ce-a7e2-afc5a9443e94)
![Screenshot (37)](https://github.com/user-attachments/assets/541dc584-28b5-4e4b-91d7-1769b3b1ac7a)
![Screenshot (39)](https://github.com/user-attachments/assets/12beeb1c-025f-4c93-86f3-8dc4a8708291)
![Screenshot (41)](https://github.com/user-attachments/assets/35fed1d2-2a13-47bb-9a34-4da772467ef6)
![Screenshot (40)](https://github.com/user-attachments/assets/b6260128-9e57-4dc6-8a4a-8dcaabf53e61)
![Screenshot (81)](https://github.com/user-attachments/assets/8ca62c9d-b9b7-43b1-9661-ef64515411ee)
![Screenshot (42)](https://github.com/user-attachments/assets/9c82e5b1-902c-4419-b532-00b282043267)
![Screenshot (80)](https://github.com/user-attachments/assets/9b18f62a-c042-45d0-9210-3bf563c7a3b5)



---

## 🛠️ Technologies Used

- Language: **C#**
- GUI: **Windows Forms (WinForms)**
- IDE: **Visual Studio**
- Logic Layer: Custom-built DSA logic

---

## 📂 Folder & File Structure

```plaintext
📦 SuperMarket-Management-System
├── Sorting Algorithms
│   ├── BubbleSort.cs
│   ├── InsertionSort.cs
│   ├── MergeSort.cs
│   ├── QuickSort.cs
│   └── SelectionSort.cs
├── Data Structures
│   ├── LinkedList.cs
│   ├── BinarySearchTree.cs
│   └── DynamicArray.cs
├── Forms
│   ├── FormUser.cs
│   ├── FormUserEdit.cs
│   ├── FormUserSort.cs
│   ├── FormItem.cs
│   ├── FormItemEdit.cs
│   ├── FormLogin.cs
│   └── MainForm.cs
├── Database
│   ├── UserDatabase.cs
│   └── ItemDatabase.cs
├── App.config
├── Program.cs
├── SuperMarket-Management-System.sln
└── SuperMarket-Management-System.csproj

```
---

## 👨‍💻 Authors
- Dhanuja Kahatapitiya 
- Nethmini Herath
- Irusha Sansuka

