import { useState } from "react";
import { login } from "../api/auth.api";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";
import "../CSS/Login.css";
import backgroundImage from "../assets/fishing-bg.jpg";

const Login = () => 
    {
        const navigate = useNavigate();
        const [formData, setFormData] = useState({email: '', password: ''});
        const { setUser } = useAuth();

        const handleSubmit = async(e) =>
        {
            e.preventDefault();
            try
            {
                const data = await login(formData);
                localStorage.setItem('accessToken', data.accessToken);
                localStorage.setItem('refreshToken', data.refreshToken);
                setUser(data.user)
                toast.success('Успішний вхід!');
                navigate('/MainPage');
            }
            catch(err)
            {
                alert(err.response?.data || "Помилка зв'язку з сервером");
            }
        };

        return (
        <div className="login-container" style={{ backgroundImage: `url(${backgroundImage})` }}>
            <div className="login-card">
                <h1>Вхід</h1>
                <form onSubmit={handleSubmit} className="login-form">
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
                    <button type="submit" className="login-button">Увійти</button>
                </form>
                <p className="register-link">
                    Немає акаунту? <Link to="/register">Зареєструватися</Link>
                </p>
            </div>
        </div>
    );
        
    };

export default Login;