using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class storyScene : MonoBehaviour
{
	public PlayableDirector timelineDirector; // Gán PlayableDirector từ Inspector
	public string nextSceneName; // Tên scene muốn chuyển đến
	public GameObject button;

	void Start()
	{
		if (timelineDirector != null)
		{
			timelineDirector.stopped += OnTimelineFinished; // Đăng ký sự kiện
		}
	}

	void OnTimelineFinished(PlayableDirector director)
	{
		if (director == timelineDirector) // Kiểm tra đúng timeline
		{
			button.SetActive(true); 
		}
	}

	void OnDestroy()
	{
		if (timelineDirector != null)
		{
			timelineDirector.stopped -= OnTimelineFinished; // Hủy đăng ký sự kiện
		}
	}

	private void Awake()
	{
		button.SetActive(false);
	}
}
