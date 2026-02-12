import { useState } from "react";
import axios from 'axios';
import {useNavigate} from 'react-router-dom';
import { useAuth } from "../context/AuthContext";

const MainPage = () =>
    {
        const {user, logout} = useAuth();
        const navigate = useNavigate();

        const handleLogout = () =>
        {
            logout(); 
            navigate("/login");
        };
    
    

    
        return(
            <div style={{ padding: "20px" }}>
            <header style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <h1>Catch Tracker</h1>
                <div className="user-info">
                    {/* getMe підтягнув нам ім'я користувача */}
                    <span>Вітаємо, <strong>{user?.username || user?.name}</strong>!</span>
                    <button onClick={handleLogout} style={{ marginLeft: "15px", cursor: "pointer" }}>
                        Вийти
                    </button>
                </div>
            </header>
            
            <main style={{ marginTop: "25px" }}>
                <h2>Тут буде твій основний контент</h2>
            </main>
        </div>
        );
        
    };
export default MainPage;