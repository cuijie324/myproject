from django import forms
from .models import Topic, Entry


class TopicForm(forms.ModelForm):

    # 告诉django根哪个模型创建表单
    class Meta:
        model = Topic
        fields = ['text']  # 包含的字段
        labels = {'text': '主题'}  # 字段的标签


class EntryForm(forms.ModelForm):

    class Meta:
        model = Entry
        fields = ['text']
        labels = {'text': ''}
        widgets = {'text': forms.Textarea(attrs={'cols': 80})}
