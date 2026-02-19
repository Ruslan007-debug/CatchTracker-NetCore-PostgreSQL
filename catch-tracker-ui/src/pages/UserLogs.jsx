import {useState, useEffect} from "react";
import { useNavigate } from "react-router-dom";
import { getMyLogs } from "../api/fishingLogs.api";
import '../CSS/UserLogs.css';


const UserLogs = ()=>
    {
        const navigate = useNavigate();
        const [userLogs, setUserLogs] = useState([]);
        const[loading, setLoading] = useState(false);

        useEffect(()=>{
            const loadLogs =async()=>
                {
                    setLoading(true);
                    try
                    {
                        const data = await getMyLogs();
                        setUserLogs(data);
                    }
                    catch(error)
                    {
                        console.error("Помилка завантаження логів:", error);
                    }
                    finally
                    {
                        setLoading(false)
                    }
                };
                loadLogs();
        },[]);

        return (
            <div className="my-logs-page">
                <header className="logs-header">
                    <h1>🎣 Мій щоденник уловів</h1>
                    <button onClick={() => navigate('/MainPage')} className="back-home-btn">
                        На головну
                    </button>
                </header>

                {loading ? (
                    <div className="loader">⏳ Завантаження ваших трофеїв...</div>
                ) : (
                    <div className="logs-grid">
                        {userLogs.length === 0 ? (
                            <p className="no-logs">Ви ще не зафіксували жодного улову. Час на риболовлю!</p>
                        ) : (
                            userLogs.map((log) => (
                                <div key={log.id} className="fishing-card">
                                    {/* Фото місця лову як фон верхньої частини */}
                                    <div 
                                        className="card-banner" 
                                        style={{ backgroundImage: `url(${log.place?.imgUrl})` }}
                                    >
                                        <div className="catch-date">
                                            {new Date(log.time).toLocaleDateString('uk-UA')}
                                        </div>
                                        {/* Кругле фото риби */}
                                        <div className="fish-avatar">
                                            <img src={log.fishType?.imageUrl} alt={log.fishType?.typeName} />
                                        </div>
                                    </div>

                                    <div className="card-body">
                                        <div className="main-title">
                                            <h2>{log.fishType?.typeName}</h2>
                                            <span className="weight-badge">{log.weight} кг</span>
                                        </div>
                                        
                                        <p className="location-text">📍 {log.place?.name}</p>

                                        <div className="info-grid">
                                            <div className="info-item">
                                                <span className="label">Наживка</span>
                                                <span className="value">{log.bait}</span>
                                            </div>
                                            <div className="info-item">
                                                <span className="label">Дистанція</span>
                                                <span className="value">{log.distance} м</span>
                                            </div>
                                            <div className="info-item">
                                                <span className="label">Трофей</span>
                                                <span className="value">{log.trophy}</span>
                                            </div>
                                            <div className="info-item">
                                                <span className="label">Темп. води</span>
                                                <span className="value">{log.place?.waterTemp}°C</span>
                                            </div>
                                        </div>

                                        <div className="card-footer">
                                            <p className="place-desc">{log.place?.description}</p>
                                        </div>
                                    </div>
                                </div>
                            ))
                        )}
                    </div>
                )}
            </div>
        );
    }
export default UserLogs;
