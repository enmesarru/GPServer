import { shallowMount } from '@vue/test-utils'
import AppNavbar from '@/components/shared/AppNavbar'

import { shallowMount } from '@vue/test-utils'
import HelloWorld from '@/components/HelloWorld.vue'

describe('HelloWorld.vue', () => {
  it('renders props.msg when passed', () => {
    const msg = 'new message'
    const wrapper = shallowMount(AppNavbar);
    expect(wrapper.text()).toMatch(msg)
  })
})