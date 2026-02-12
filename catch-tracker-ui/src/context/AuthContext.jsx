import { createContext, useContext, useState, useEffect } from "react";
import { getMe } from "../api/auth.api";

const AuthContext = createContext();

export const AuthProvider = ({children}) => 
    {
        const [user, setUser] = useState(null);
        const [loading, setLoading] = useState(true);

        const initAuth = async () =>
            {
                try
                {
                    const token = localStorage.getItem('accessToken');
                    if(token)
                    {
                        const data = await getMe();
                        setUser(data);
                    }
                }
                catch(e)
                {
                    localStorage.clear();
                }
                finally
                {
                    setLoading(false);
                }
            };
        
        useEffect(() => {initAuth();}, []);
        const logout = () =>
            {
                localStorage.clear();
                setUser(null);
            };

        return (
            <AuthContext.Provider value={{ user, setUser, logout, loading }}>
            {children}
            </AuthContext.Provider>);
    }

export const useAuth = () => useContext(AuthContext);