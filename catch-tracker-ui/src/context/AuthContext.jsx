import { createContext, useContext, useState, useEffect } from "react";
import { getMe } from "../api/auth.api";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    const initAuth = async () => {
        try {
            const token = localStorage.getItem('accessToken');
            if (token) {
                const data = await getMe();
                setUser(data);
            }
        }
        catch (e) {
            setUser(null);
        }
        finally {
            setLoading(false);
        }
    };

    useEffect(() => { initAuth(); }, []);

    const logout = async () => 
        {
        try 
        {
            const refreshToken = localStorage.getItem('refreshToken');
            if (refreshToken) {

                await import('../api/auth.api').then(m => m.logout?.({ refreshToken }));
            }
        }
        catch (e) 
        {

        }
        finally 
        {
            localStorage.clear();
            setUser(null);
        }
    };

    return (
        <AuthContext.Provider value={{ user, setUser, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);