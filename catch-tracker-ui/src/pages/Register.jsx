import { useState } from "react";
import { register, getMe } from "../api/auth.api"; 
import { useAuth } from "../context/AuthContext";
import {useNavigate} from 'react-router-dom';
import { toast } from 'react-toastify';
import api from "../api/axios";
import { Link } from "react-router-dom";
import "../CSS/Register.css";
import backgroundImage from "../assets/fishing-bg.jpg";

const Register = () =>
{
    const navigate = useNavigate();
    const { setUser } = useAuth();
    const [formData, setFormData] = useState({name: '', email: '', password: '', confirmPassword: ''});

    const handleSubmit = async (e)=>
    {
        e.preventDefault();

        if (formData.password !== formData.confirmPassword) {
            return toast.error("Паролі не співпадають!");
        }

        try
        {
            const data = await register(formData);
            localStorage.setItem('accessToken', data.accessToken);
            localStorage.setItem('refreshToken', data.refreshToken);
            const userDoc = await getMe();
            setUser(userDoc);
            toast.success('Успішна реєстрація!');
            navigate('/MainPage');
        }
        catch(err)
        {
            alert(err.response?.data || "Помилка зв'язку з сервером");
        }
    };


    return (
        
        <div className="register-container" style={{ backgroundImage: `url(${backgroundImage})` }}>
            <div className="register-card">
                <h1>Реєстрація</h1>
                <form onSubmit={handleSubmit} className="register-form">
                    <input 
                        type="text" 
                        placeholder="Ім`я" 
                        required 
                        onChange={(e) => setFormData({...formData, name: e.target.value})}
                    />
                    <input 
                        type="email" 
                        placeholder="Пошта" 
                        required 
                        onChange={(e) => setFormData({...formData, email: e.target.value})}
                    />
                    <input 
                        type="password" 
                        placeholder="Пароль" 
                        required 
                        onChange={(e) => setFormData({...formData, password: e.target.value})}
                    />
                    <input 
                        type="password" 
                        placeholder="Підтвердження паролю" 
                        required 
                        onChange={(e) => setFormData({...formData, confirmPassword: e.target.value})}
                    />
                    <button type="submit" className="register-button">Створити акаунт</button>
                </form>
                <p className="login-link">
                    Вже є акаунт? <Link to="/login">Увійти</Link>
                </p>
            </div>
        </div>
    );
};
export default Register;