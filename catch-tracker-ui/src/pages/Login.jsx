import { useState } from "react";
import { login } from "../api/auth.api";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { Link } from "react-router-dom";

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

        return(
            <div style={{ padding: '20px', maxWidth: '400px' }}>
                <h1>Вхід</h1>
                <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                    <input type="email" placeholder="Пошта" required
                    onChange={(e) => setFormData({...formData, email: e.target.value})}/>
                    <input type="password" placeholder="Пароль" required
                    onChange={(e)=>setFormData({...formData, password:e.target.value})}/>
                    <button type="submit" style={{ cursor: 'pointer' }}>Увійти</button>
                </form>
                <p style={{ marginTop: '15px' }}>
                Немає акаунту? <Link to="/register">Зареєструватися</Link>
            </p>
            </div>
        );
        
    };

export default Login;