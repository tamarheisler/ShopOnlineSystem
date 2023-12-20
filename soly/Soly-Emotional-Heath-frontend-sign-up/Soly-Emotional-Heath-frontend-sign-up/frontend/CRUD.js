import React, { useState, useEffect } from 'react';

export default function CRUD() {
    const [users, setUsers] = useState([]);

    // Demo array of users
    useEffect(() => {
        const demoUsers = [
            { email: 'user1@example.com', password: 'password1' },
            { email: 'user2@example.com', password: 'password2' },
            { email: 'user3@example.com', password: 'password3' },
        ];
        setUsers(demoUsers);
    }, []);
    const create_user = (user) => {
        try {
            let allUsers = [...users];
            allUsers.push(user);
            setUsers(allUsers);
        } catch (error) {
            console.error('An error occurred while creating a user:', error);
        }
    };

    const read_user = (email) => {
        try {
            let allUsers = [...users];
            for (const user in allUsers) {
                if (user.email === email) {
                    return user;
                }
            }
            throw new Error("user not found");
        } catch (error) {
            console.error('An error occurred while getting user:', error);
        }
    };

    const read_users = () => {
        try {
            let allUsers = [...users];
            return allUsers;
        } catch (error) {
            console.error('An error occurred while getting all users:', error);
        }
    };

    const update_user = (user) => {
        try {
            let allUsers = [...users];
            for (const suser in allUsers) {
                if (suser.email === user.email) {
                    allUsers.splice(allUsers.indexOf(suser), 1);
                    allUsers.push(user);
                    setUsers(allUsers);
                    return;
                }
            }
            throw new Error("user not found");
        } catch (error) {
            console.error('An error occurred while updating user:' + error.message);
        }
    };

    const delete_user = (email) => {
        try {
            let allUsers = [...users];
            for (const user in allUsers) {
                if (user.email === email) {
                    allUsers.splice(allUsers.indexOf(user), 1);
                    setUsers(allUsers);
                    return;
                }
            }
            throw new Error("user not found");
        } catch (error) {
            console.error('An error occurred while deleting user:', error);
        }
    }


    return (
        <div>
            <div>
                <h2>All User List</h2>
                <ul>
                    {read_users().map((user, index) => (
                        <li key={index}>
                            <strong>Email:</strong> {user.email}, <strong>Password:</strong> {user.password}
                        </li>
                    ))}
                </ul>
            </div>
            <div>
                <h2>specific user</h2>
                <ul>
                    {read_user('user1@example.com').map((user, index) => (
                        <li key={index}>
                            <strong>Email:</strong> {user.email}, <strong>Password:</strong> {user.password}
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
}
