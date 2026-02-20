import { useState, useEffect, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { getLeaderboard } from "../api/fishingLogs.api";
import '../CSS/LeaderBoard.css';

const LeaderBoard = () => {
    const navigate = useNavigate();
    const [leaderboard, setLeaderboard] = useState([]);
    const [loading, setLoading] = useState(false);
    const [selectedFish, setSelectedFish] = useState("Всі");

    const loadData = async () => {
        setLoading(true);
        try {
            // Отримуємо актуальний топ-100 уловів
            const data = await getLeaderboard(100);
            setLeaderboard(data);
        } catch (error) {
            console.error('Помилка завантаження:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => { loadData() }, []);

    // ЦЕ КЛЮЧОВИЙ МОМЕНТ:
    // Ми беремо всі унікальні назви риб, які ВЖЕ є в отриманному списку рекордів.
    // Якщо ви додали Лина, і він потрапив у топ-100, він автоматично з'явиться тут.
    const dynamicFishTypes = useMemo(() => {
        const names = leaderboard.map(log => log.fishTypeName);
        // Set прибере дублікати, а filter прибере пусті значення
        return ["Всі", ...new Set(names)].filter(Boolean).sort();
    }, [leaderboard]);

    const filteredData = leaderboard.filter(log => {
        if (selectedFish === "Всі") return true;
        return log.fishTypeName === selectedFish;
    });

    return (
        <div className="leaderboard-page">
            <div className="page-header">
                <div className="header-content">
                    <h1>🏆 Таблиця лідерів</h1>
                    <p className="header-subtitle">Тут відображаються тільки рекордні улови</p>
                </div>
                
                <div className="header-controls">
                    <div className="filter-group">
                        <label>Вид риби серед рекордів:</label>
                        <select 
                            value={selectedFish} 
                            onChange={(e) => setSelectedFish(e.target.value)}
                            className="fish-select"
                        >
                            {dynamicFishTypes.map(name => (
                                <option key={name} value={name}>
                                    {name === "Всі" ? "✨ Всі види" : `🐟 ${name}`}
                                </option>
                            ))}
                        </select>
                    </div>
                    
                    <button onClick={() => navigate('/MainPage')} className="back-btn">
                        ← На головну
                    </button>
                </div>
            </div>

            {loading ? (
                <div className="leaderboard-loader">
                    <div className="spinner"></div>
                    <p>Оновлюємо рекорди...</p>
                </div>
            ) : (
                <div className="leaderboard-container">
                    <div className="table-wrapper">
                        <table className="leaderboard-table">
                            <thead>
                                <tr>
                                    <th>Місце</th>
                                    <th>Риболов</th>
                                    <th>Вид риби</th>
                                    <th>Вага</th>
                                    <th>Місце лову</th>
                                    <th>Дата</th>
                                </tr>
                            </thead>
                            <tbody>
                                {filteredData.length === 0 ? (
                                    <tr>
                                        <td colSpan="6" className="no-data">
                                            <div className="empty-state">
                                                <span>🐢</span>
                                                <p>У категорії "{selectedFish}" поки порожньо.</p>
                                            </div>
                                        </td>
                                    </tr>
                                ) : (
                                    filteredData.map((log, index) => (
                                        <tr key={index} className={`row-rank-${index + 1}`}>
                                            <td className="rank-cell">
                                                {index === 0 ? "🥇" : index === 1 ? "🥈" : index === 2 ? "🥉" : index + 1}
                                            </td>
                                            <td className="user-cell"><strong>{log.userName}</strong></td>
                                            <td className="fish-cell">{log.fishTypeName}</td>
                                            <td className="weight-cell">
                                                <span className="weight-badge">{log.weight} кг</span>
                                            </td>
                                            <td className="place-cell">📍 {log.placeName}</td>
                                            <td className="date-cell">
                                                {new Date(log.caughtAt).toLocaleDateString('uk-UA')}
                                            </td>
                                        </tr>
                                    ))
                                )}
                            </tbody>
                        </table>
                    </div>
                </div>
            )}
        </div>
    );
}

export default LeaderBoard;