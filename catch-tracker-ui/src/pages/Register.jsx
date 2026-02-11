import { useState } from "react";
import axios from 'axios';
import {useNavigate} from 'react-router-dom';

const Register = () =>
{
    const navigate = useNavigate();
    const [formData, setFormData] = useState({name: '', email: '', password: '', confirmPassword: ''});

    const handleSubmit = async (e)=>
    {
        e.preventDefault();
        try
        {
            const response = await axios.post("https://localhost:7071/api/auth/register", formData);
            localStorage.setItem('token', response.data.accessToken)
            alert('Реєстрація успішна!');
            navigate('/login');
        }
        catch(err)
        {
            alert(err.response?.data || "Помилка зв'язку з сервером");
        }
    };


    return(
        <div style={{ padding: '20px', maxWidth: '400px' }}>
            <h1>Реєстрація</h1>
            <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                <input type="text" placeholder="Ім`я" required 
                onChange={(e)=> setFormData({... formData, name: e.target.value})}/>
                <input type="email" placeholder="Пошта" required 
                onChange={(e)=> setFormData({... formData, email: e.target.value})}/>
                <input type="password" placeholder="Пароль" required 
                onChange={(e)=> setFormData({... formData, password: e.target.value})}/>
                <input type="password" placeholder="Підтвердження паролю" required 
                onChange={(e)=> setFormData({... formData, confirmPassword: e.target.value})}/>
                <button type="submit" style={{ cursor: 'pointer' }}>Створити акаунт</button>
            </form>

        </div>
    );
};
export default Register;