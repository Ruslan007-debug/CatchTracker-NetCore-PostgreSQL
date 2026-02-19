import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getLeaderboard } from "../api/fishingLogs.api";
import '../CSS/LeaderBoard.css';



const LeaderBoard =()=>
    {
        const navigate = useNavigate();
        const [leaderboard, setLeaderboard] = useState([]);
        const [loading, setLoading] = useState(false);
        const [selectedFish, setSelectedFish] = useState("Всі");

        const loadLeaderboard=async()=>
            {
                setLoading(true);
                try
                {
                    const data = await getLeaderboard(50);
                    setLeaderboard(data);

                }
                catch(error)
                {
                    console.error('Помилка:', error);
                }
                finally
                {
                    setLoading(false);
                }
            };
            useEffect(()=>{loadLeaderboard()},[]);

            
        const filteredData = leaderboard.filter(log => 
            {
        if (selectedFish === "Всі") return true;
        // Приводимо до нижнього регістру для надійного порівняння
        return log.fishTypeName.toLowerCase().includes(selectedFish.toLowerCase());
            });

        return (
            <div className="leaderboard-page">
                <div className="page-header">
                    <h1>🏆 Таблиця лідерів</h1>
                    <div className="header-controls">
                        {/* 3. Випадаючий список */}
                        <div className="filter-container">
                            <label htmlFor="fish-filter">Фільтр по рибі: </label>
                            <select 
                                id="fish-filter"
                                value={selectedFish} 
                                onChange={(e) => setSelectedFish(e.target.value)}
                                className="fish-select"
                            >
                                <option value="Всі">Всі види</option>
                                <option value="Короп">Короп</option>
                                <option value="Окунь">Окунь</option>
                                <option value="Щука">Щука</option>
                                <option value="Судак">Судак</option>
                            </select>
                        </div>
                        
                        <button onClick={() => navigate('/MainPage')} className="back-btn">
                            ← На головну
                        </button>
                    </div>
                </div>

                {loading ? (
                    <div className="loading"><p>⏳ Завантаження...</p></div>
                ) : (
                    <div className="leaderboard-container">
                        <table className="leaderboard-table">
                            <thead>
                                <tr>
                                    <th>🏅 Місце</th>
                                    <th>👤 Риболов</th>
                                    <th>🐟 Вид риби</th>
                                    <th>⚖️ Вага (кг)</th>
                                    <th>🏞️ Місце</th>
                                    <th>📅 Дата</th>
                                </tr>
                            </thead>
                            <tbody>
                                {filteredData.length === 0 ? (
                                    <tr>
                                        <td colSpan="6" className="no-data">😕 Записів не знайдено</td>
                                    </tr>
                                ) : (
                                    // 4. Використовуємо filteredData замість leaderboard
                                    filteredData.map((log, index) => (
                                        <tr key={index} className={index === 0 ? 'gold' : index === 1 ? 'silver' : index === 2 ? 'bronze' : ''}>
                                            <td className="rank">
                                                {index === 0 ? "🥇" : index === 1 ? "🥈" : index === 2 ? "🥉" : index + 1}
                                            </td>
                                            <td className="username">{log.userName}</td>
                                            <td>{log.fishTypeName}</td>
                                            <td className="weight">{log.weight} кг</td>
                                            <td>{log.placeName}</td>
                                            <td className="date">
                                                {new Date(log.caughtAt).toLocaleDateString('uk-UA')}
                                            </td>
                                        </tr>
                                    ))
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        );
    }

export default LeaderBoard;