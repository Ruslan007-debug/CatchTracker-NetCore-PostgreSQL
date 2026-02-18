import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './context/AuthContext';
import Register from './pages/Register';
import Login from './pages/Login';
import MainPage from './pages/MainPage';
import { ToastContainer } from 'react-toastify';
import FishTypes from './pages/FishTypes';
import Places from './pages/Places';
import LeaderBoard from './pages/LeaderBoard';

// Захист: Тільки для тих, хто НЕ залогінився (Login, Register)
const PublicRoute = ({ children }) => {
    const { user, loading } = useAuth();
    if (loading) return null; // Чекаємо, поки getMe перевірить токен
    return !user ? children : <Navigate to="/MainPage" replace />;
};

// Захист: Тільки для АВТОРИЗОВАНИХ (MainPage, Profile)
const ProtectedRoute = ({ children }) => {
    const { user, loading } = useAuth();
    if (loading) return null; 
    return user ? children : <Navigate to="/login" replace />;
};

function App() {
  
    return (
        <AuthProvider> {/* 1. Контекст обгортає все */}
            <Router>
                <Routes>
                    {/* Публічні маршрути */}
                    <Route path="/register" element={
                        <PublicRoute> <Register /> </PublicRoute>
                    } />
                    <Route path="/login" element={
                        <PublicRoute> <Login /> </PublicRoute>
                    } />

                    {/* Захищені маршрути */}
                    <Route path="/MainPage" element={
                        <ProtectedRoute> <MainPage /> </ProtectedRoute>
                    } />
                    <Route path="/fishTypes" element={
                        <ProtectedRoute> <FishTypes /> </ProtectedRoute>
                    } />
                    <Route path="/places" element={
                        <ProtectedRoute> <Places /> </ProtectedRoute>
                    } />
                    <Route path="/leaderboard" element={
                        <ProtectedRoute><LeaderBoard /></ProtectedRoute>
                    } />

                    {/* Автоматичний редірект: якщо зайшов на "/", кидаємо на головну */}
                    <Route path="/" element={<Navigate to="/MainPage" replace />} />
                    
                    {/* 404: якщо сторінку не знайдено */}
                    <Route path="*" element={<Navigate to="/" replace />} />
                </Routes>
            </Router>
            <ToastContainer position="top-right" autoClose={1500} theme="colored" />
        </AuthProvider>
    );
}

export default App;